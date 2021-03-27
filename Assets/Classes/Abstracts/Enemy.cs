using System.Collections.Generic;
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

        [Header("Other")]
        public GameObject MinimapIcon;

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

        private void Start()
        {
            PlayerRoom = RoomsHandler.Instance.PlayerRoom;

            RoomsWeights = RoomsHandler.Instance.CalculateWeights(PlayerRoom);
        }

        private void Update()
        {
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
            CurrentAttackPower += Time.deltaTime * GameHandler.Instance.CurrentDangerLevel;

            if (CurrentAttackPower >= AttackPower)
            {
                SuccessfulAttack();
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

            RoomsWeights = RoomsHandler.Instance.CalculateWeights(destination);

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
                if (!audioDecoy.IsDestroyed && audioDecoy.IsPlaying)
                {
                    audioDecoy.DestroyDecoy();
                }
            }
        }

        protected void ChangeToAttackMode()
        {
            IsAttacking = true;
        }

        private void SuccessfulAttack()
        {
            EventHandler.Instance.Lose();
        }

        private void FailAttack()
        {
            IsAttacking = false;
            CurrentAttackPower = 0f;
            CurrentAttackingTime = 0f;

            ChangeRoom(SpawnRoom);
            Debug.Log($"{gameObject.name}: I've failed the attack on player. Teleporting to {SpawnRoom.name}.");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == AudioDecoyTag)
            {
                AudioDecoyColliderInRange = other;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == AudioDecoyTag)
            {
                AudioDecoyColliderInRange = null;
            }
        }

        private bool IsInDecoyRange()
        {
            return true;
        }

        protected abstract void Move();
    }
}