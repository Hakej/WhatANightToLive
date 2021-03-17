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

        [HideInInspector]
        public int CurrentDifficulty;
        [HideInInspector]
        public Room SpawnRoom;

        [Header("Chance of successful attack on a failed attack")]
        public float ChanceOnSanityAbove50 = 0f;
        public float ChanceOnSanityBelow50 = 1f;
        public float ChanceOnSanityBelow25 = 2f;
        public float ChanceOnSanityBelow1 = 100f;

        [Header("Enemy attack's strength")]
        public float AttackPower;
        public float AttackingTime;

        [HideInInspector]
        public Room CurrentRoom;
        [HideInInspector]
        public bool IsAttacking = false;
        [HideInInspector]
        public float CurrentAttackPower = 0f;
        [HideInInspector]
        public float CurrentAttackingTime = 0f;

        private float _currentCooldown = 0f;

        public float CurrentChanceOfPowerAttack { get; private set; }

        public void Spawn(EnemySpawnInfo enemySpawnInfo)
        {
            if (StartingDifficulty == 0)
            {
                Destroy(gameObject);
                return;
            }

            SpawnRoom = enemySpawnInfo.SpawnRoom;

            CurrentRoom = SpawnRoom;
            CurrentDifficulty = StartingDifficulty;
            transform.position = SpawnRoom.transform.position;

            EventHandler.Instance.OnPlayerSanityCrossing50 += OnPlayerSanityCrossing50;
            EventHandler.Instance.OnPlayerSanityCrossing25 += OnPlayerSanityCrossing25;
            EventHandler.Instance.OnPlayerSanityCrossing1 += OnPlayerSanityCrossing1;

            gameObject.SetActive(true);

            CurrentChanceOfPowerAttack = ChanceOnSanityAbove50;
        }

        private void OnPlayerSanityCrossing50(bool isBelow)
        {
            CurrentChanceOfPowerAttack = isBelow ? ChanceOnSanityBelow50 : ChanceOnSanityAbove50;
            CurrentDifficulty = isBelow ? CurrentDifficulty + 1 : CurrentDifficulty - 1;
        }

        private void OnPlayerSanityCrossing25(bool isBelow)
        {
            CurrentChanceOfPowerAttack = isBelow ? ChanceOnSanityBelow25 : ChanceOnSanityBelow50;
            CurrentDifficulty = isBelow ? CurrentDifficulty + 1 : CurrentDifficulty - 1;
        }

        private void OnPlayerSanityCrossing1(bool isBelow)
        {
            CurrentChanceOfPowerAttack = isBelow ? ChanceOnSanityBelow1 : ChanceOnSanityBelow25;
            CurrentDifficulty = isBelow ? CurrentDifficulty + 1 : CurrentDifficulty - 1;
        }

        protected void ChangeRoom(Room newRoom)
        {
            EventHandler.Instance.EnemyChangingRoom(CurrentRoom, newRoom);

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
            Debug.Log($"{gameObject.name}: I've failed the attack on player. Teleporting to {SpawnRoom.name}.");
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

            var chance = Random.Range(0f, 100f);

            if (chance < CurrentChanceOfPowerAttack)
            {
                SuccessfulAttack();
            }
            else
            {
                FailAttack();
            }
        }

        private void CheckMove()
        {
            _currentCooldown += Time.deltaTime;

            if (_currentCooldown < MoveCooldown)
            {
                return;
            }

            _currentCooldown = 0f;

            var randomNumber = Random.Range(1, 20);

            if (CurrentDifficulty <= randomNumber)
            {
                Debug.Log($"{gameObject.name}: I've failed my movement.");
                return;
            }

            Move();
        }

        protected abstract void Move();
    }
}