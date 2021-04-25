using System.Collections;
using System.Collections.Generic;
using Assets.Classes.Unity;
using Classes.Extensions;
using Controllers;
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
        [TagSelector]
        public string AudioDecoyTag;
        [HideInInspector]
        public Collider AudioDecoyColliderInRange;

        [Header("Enemy attack's strength")]
        public float AttackPower;
        public float AttackingTime;
        public float LowSanityThreshold = 10f;

        [Header("Vent")]
        public bool IgnoreVents = true;
        public bool IgnoreAdjacentRooms = false;

        [Header("Jumpscare Mechanisms")]
        [TagSelector]
        public string FlashlightTag = "";
        [TagSelector]
        public string PlayerLookTag = "";
        [TagSelector]
        public string MapRightSideTag = "";
        [TagSelector]
        public string PlayerAdjacentRoomTag = "";

        [Header("Enemy audio")]
        public AudioClip JumpscareAudio;
        public AudioSource JumpscareAudioSource;
        public AudioClip[] MovementAudioClips;
        public AudioClip[] VentMovementAudioClips;
        public AudioSource MovementAudioSource;
        public AudioClip[] CloseToPlayerAudioClips;
        public AudioSource CloseToPlayerAudioSource;

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
        [HideInInspector]
        public bool IsPlayerFacingMe = false;

        private void Start()
        {
            PlayerRoom = RoomsHandler.Instance.PlayerRoom;

            RoomsWeights = RoomsHandler.Instance.CalculateWeights(PlayerRoom, IgnoreVents, IgnoreAdjacentRooms);

            EventHandler.Instance.OnLose += OnLose;
        }

        private void OnLose()
        {
            Destroy(this);
        }

        private void RunAway()
        {
            IsRunningAway = true;
            Animator.SetBool("IsRunningAway", true);

            MovementAudioSource.pitch = Random.Range(0.8f, 1.2f);

            PlayMovementSound();

            Invoke("FinishRunAway", 1f);
        }

        protected void PlayMovementSound()
        {
            MovementAudioSource.pitch = Random.Range(0.8f, 1.2f);

            AudioClip clipToPlay = null;

            if (CurrentRoom.IsVent)
            {
                clipToPlay = VentMovementAudioClips.GetRandomElement();
            }
            else
            {
                clipToPlay = MovementAudioClips.GetRandomElement();
            }

            if (clipToPlay != null)
            {
                MovementAudioSource.PlayOneShot(clipToPlay);
            }
        }

        private void FinishRunAway()
        {
            IsRunningAway = false;
            Animator.SetBool("IsRunningAway", false);

            CloseToPlayerAudioSource.volume = 0f;

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
            if (!CloseToPlayerAudioSource.isPlaying)
            {
                CloseToPlayerAudioSource.PlayOneShot(CloseToPlayerAudioClips.GetRandomElement());
            }

            if (GameHandler.Instance.IsPowerOn || SanityHandler.Instance.CurrentSanity <= LowSanityThreshold)
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

            if (AudioDecoyColliderInRange && AudioDecoyColliderInRange.enabled)
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

        public void ChangeRoom(Room newRoom)
        {
            EventHandler.Instance.EnemyChangingRoom(this, CurrentRoom, newRoom);
            CurrentRoom = newRoom;
            transform.position = newRoom.transform.position;

            var audioDecoy = newRoom.AudioDecoy;

            if (audioDecoy)
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
            CurrentMoveCooldown = 0f;

            CloseToPlayerAudioSource.volume = IsPlayerFacingMe ? 0.4f : 0.05f;
        }

        private void TryAttack()
        {
            var door = CurrentRoom.Door;

            if (door && door.IsClosed)
            {
                FailAttack();
                return;
            }

            SuccessfulAttack();
        }

        public void SuccessfulAttack()
        {
            EventHandler.Instance.SuccessfulAttack(IsPlayerFacingMe);

            IsAttacking = false;

            if (!IsPlayerFacingMe)
            {
                var delay = PlayerController.Instance.JumpscareRotationSpeed;

                PlayerController.Instance.RotateToFaceEnemy(transform.position);

                StartCoroutine(Attack(delay));
            }
            else
            {
                StartCoroutine(Attack());
            }
        }

        private IEnumerator Attack(float delayToAttack = 0f)
        {
            yield return new WaitForSeconds(delayToAttack);

            Animator.SetBool("IsAttacking", true);

            JumpscareAudioSource.PlayOneShot(JumpscareAudio);

            yield return new WaitForSeconds(0.5f);

            AfterAttack();
        }

        private void AfterAttack()
        {
            EventHandler.Instance.Lose();
        }

        private void FailAttack()
        {
            var door = CurrentRoom.Door;

            if (door && door.IsClosed)
            {
                // TODO: It should be handled better, but it requires a rework of how room handles disabled features.
                door.MakeAttackNoise();
                EventHandler.Instance.DoorHit(door);
            }

            RunAway();

            Debug.Log($"{gameObject.name}: I've failed the attack on player. Teleporting to {SpawnRoom.name}.");
        }

        private void LitByFlashLight()
        {
            if (IsAttacking)
            {
                SuccessfulAttack();
            }
            else
            {
                RunAway();
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
            else if (other.tag == PlayerLookTag)
            {
                IsPlayerFacingMe = true;

                CloseToPlayerAudioSource.volume = IsAttacking ? 0.4f : 0f;
            }
            else if (other.tag == MapRightSideTag)
            {
                Animator.SetBool("IsApproachingFromRight", true);
                transform.Rotate(Vector3.up * 180);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == AudioDecoyTag)
            {
                AudioDecoyColliderInRange = null;
            }
            else if (other.tag == PlayerLookTag)
            {
                IsPlayerFacingMe = false;
                CloseToPlayerAudioSource.volume = IsAttacking ? 0.05f : 0f;
            }
            else if (other.tag == MapRightSideTag)
            {
                Animator.SetBool("IsApproachingFromRight", false);
                transform.Rotate(Vector3.up * -180);
            }
        }

        protected abstract void Move();
    }
}