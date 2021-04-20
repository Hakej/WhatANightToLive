﻿using System.Collections.Generic;
using Assets.Classes.Unity;
using GameObjects;
using Handlers;
using UnityEngine;

namespace Classes.Abstracts
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Enemy AI")]
        [Range(0, 20)]
        public int StartingDifficulty;
        public float MoveCooldown;

        [Header("Enemy smart move")]
        [Range(0, 1)]
        public float MinSmartMoveChance;
        [Range(0, 1)]
        public float MaxSmartMoveChance;

        [Header("Enemy foolishness by decoy")]
        public bool CanDestroyDecoy = true;
        [Range(0, 1)]
        public float MinDecoyFoolChance;
        [Range(0, 1)]
        public float MaxDecoyFoolChance;
        public bool IsCurrentlyFooled;
        public BoxCollider EnemyCollider;
        public string AudioDecoyTag;
        [HideInInspector]
        public Collider AudioDecoyColliderInRange;

        [Header("Enemy attack's strength")]
        public float AttackPower;
        public float AttackingTime;

        [Header("Vent")]
        public bool IgnoreVents = true;
        public bool IgnoreAdjacentRooms = false;

        [Header("FlashlightMechanisms")]
        [TagSelector]
        public string NextToAdjacentRoomTag = "";
        [TagSelector]
        public string FlashlightTag = "";


        [Header("Other")]
        public GameObject MinimapIcon;
        public Animator Animator;


        ///--- Hidden in inspector ---///
        [HideInInspector]
        public int CurrentDifficulty;
        [HideInInspector]
        public Room SpawnRoom;
        [HideInInspector]
        public Room CurrentRoom;
        [HideInInspector]
        public bool IsAttacking = false;
        [HideInInspector]
        public float CurrentAttackPower = 0f;
        [HideInInspector]
        public float CurrentAttackingTime = 0f;
        [HideInInspector]
        public float CurrentMoveCooldown = 0f;
        [HideInInspector]
        public Dictionary<Room, int> RoomsWeights;
        [HideInInspector]
        public Room PlayerRoom;
        [HideInInspector]
        public bool IsRunningAway = false;

        private void Start()
        {
            PlayerRoom = RoomsHandler.Instance.PlayerRoom;

            RoomsWeights = RoomsHandler.Instance.CalculateWeights(PlayerRoom, IgnoreVents, IgnoreAdjacentRooms);
        }

        private void RunAway()
        {
            IsRunningAway = true;
            Animator.SetBool("IsRunningAway", true);

            Invoke("FinishRunAway", 1f);
        }

        private void FinishRunAway()
        {
            IsRunningAway = false;
            Animator.SetBool("IsRunningAway", false);

            ResetToSpawn();
        }

        private void Update()
        {
            if (IsRunningAway)
            {
                return;
            }

            if (IsAttacking)
            {
                CheckAttack();
            }
            else
            {
                CheckMove();
            }
        }

        private void CheckAttack()
        {
            if (GameHandler.Instance.IsPowerOn)
            {
                CurrentAttackPower += Time.deltaTime;
            }

            if (CurrentAttackPower >= AttackPower)
            {
                TryAttack();
                return;
            }

            CurrentAttackingTime += Time.deltaTime;

            if (CurrentAttackingTime < AttackingTime)
            {
                return;
            }

            FailAttack();
        }

        private void CheckMove()
        {
            CurrentMoveCooldown += Time.deltaTime;

            if (CurrentMoveCooldown < MoveCooldown)
            {
                return;
            }

            CurrentMoveCooldown = 0f;

            var randomNumber = Random.Range(1, 21);

            if (CurrentDifficulty < randomNumber)
            {
                Debug.Log($"{gameObject.name}: I've failed my movement.");
                return;
            }

            var destination = PlayerRoom;

            if (AudioDecoyColliderInRange != null && AudioDecoyColliderInRange.enabled)
            {
                var randomFoolishness = Random.Range(0f, 1f);
                var currentSanitySense = SanityHandler.Instance.CurrentSanitySense;
                var currentFoolishness = Mathf.Lerp(MinDecoyFoolChance, MaxDecoyFoolChance, currentSanitySense);

                IsCurrentlyFooled = randomFoolishness <= currentFoolishness;
            }
            else
            {
                IsCurrentlyFooled = false;
            }

            if (IsCurrentlyFooled)
            {
                destination = AudioDecoyColliderInRange.GetComponent<AudioDecoy>().Room;
            }

            RoomsWeights = RoomsHandler.Instance.CalculateWeights(destination, IgnoreVents, IgnoreAdjacentRooms);

            Move();
        }

        public void Spawn(EnemySpawnInfo enemySpawnInfo)
        {
            if (StartingDifficulty == 0)
            {
                Destroy(gameObject);
                return;
            }

            SpawnRoom = enemySpawnInfo.SpawnRoom;
            CurrentRoom = enemySpawnInfo.SpawnRoom;
            CurrentDifficulty = StartingDifficulty;

            transform.position = CurrentRoom.transform.position;
        }

        protected void ChangeRoom(Room newRoom)
        {
            EventHandler.Instance.EnemyChangingRoom(this, CurrentRoom, newRoom);
            CurrentRoom = newRoom;
            transform.position = newRoom.transform.position;

            var audioDecoy = newRoom.AudioDecoy;

            if (audioDecoy != null)
            {
                if (!audioDecoy.IsDestroyed && audioDecoy.IsPlaying && CanDestroyDecoy)
                {
                    audioDecoy.DestroyDecoy();
                }
            }
        }

        protected void ChangeToAttackMode()
        {
            IsAttacking = true;
        }

        private void TryAttack()
        {
            var door = CurrentRoom.Door;

            if (door != null)
            {
                if (door.IsClosed)
                {
                    FailAttack();
                }
                else
                {
                    SuccessfulAttack();
                }
            }
            else
            {
                SuccessfulAttack();
            }
        }

        private void SuccessfulAttack()
        {
            Animator.SetBool("IsAttacking", true);

            Invoke("AfterAttack", 0.5f);
        }

        private void AfterAttack()
        {
            EventHandler.Instance.Lose();
        }

        private void FailAttack()
        {
            var door = CurrentRoom.Door;

            if (door != null)
            {
                if (door.IsClosed)
                {
                    // TODO: It should be handled better, but it requires a rework of how room handles disabled features.
                    door.MakeAttackNoise();
                    EventHandler.Instance.DoorHit(door);
                }
            }

            ResetToSpawn();

            Debug.Log($"{gameObject.name}: I've failed the attack on player. Teleporting to {SpawnRoom.name}.");
        }

        private void LitByFlashLight()
        {
            if (CurrentRoom.CompareTag(NextToAdjacentRoomTag))
            {
                RunAway();
            }
            else
            {
                SuccessfulAttack();
            }
        }

        private void ResetToSpawn()
        {
            IsAttacking = false;
            IsRunningAway = false;
            CurrentAttackPower = 0f;
            CurrentAttackingTime = 0f;

            ChangeRoom(SpawnRoom);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == AudioDecoyTag)
            {
                AudioDecoyColliderInRange = other;
            }
            else if (other.tag == FlashlightTag)
            {
                LitByFlashLight();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == AudioDecoyTag)
            {
                AudioDecoyColliderInRange = null;
            }
        }

        protected abstract void Move();
    }
}