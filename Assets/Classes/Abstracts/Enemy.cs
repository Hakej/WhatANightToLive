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
        [Range(0, 1)]
        public float BaseSmartMoveChance;
        [Range(0, 1)]
        public float MaxSmartMoveChance;

        [Header("Enemy attack's strength")]
        public float AttackPower;
        public float AttackingTime;

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
        }

        protected void ChangeRoom(Room newRoom)
        {
            EventHandler.Instance.EnemyChangingRoom(this, CurrentRoom, newRoom);
            CurrentRoom = newRoom;
            transform.position = newRoom.transform.position;
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
            ChangeRoom(SpawnRoom);
            CurrentAttackPower = 0f;
            CurrentAttackingTime = 0f;
            Debug.Log($"{gameObject.name}: I've failed the attack on player. Teleporting to {SpawnRoom.name}.");
        }

        protected abstract void Move();
    }
}