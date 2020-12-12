using Handlers;
using UnityEditor.Rendering;
using UnityEngine;

namespace Classes.Abstracts
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Spawn information")]
        public GameObject StartingRoom;
        public Classes.Time SpawningTime;

        [Header("Enemy AI")]
        [Range(0, 20)]
        public int StartingDifficulty;
        public float MoveCooldown;
        
        [Header("Chance of successful attack on a failed attack")]
        public float ChanceOnSanityAbove50 = 0f;
        public float ChanceOnSanityBelow50 = 1f;
        public float ChanceOnSanityBelow25 = 2f;
        public float ChanceOnSanityBelow1 = 100f;
        
        [Header("Enemy attack's strength")]
        public float AttackPower;
        public float AttackingTime;

        [HideInInspector]
        public GameObject CurrentRoom;
        [HideInInspector]
        public bool IsAttacking = false;
        [HideInInspector]
        public float CurrentAttackPower = 0f;
        [HideInInspector]
        public float CurrentAttackingTime = 0f;
        
        private float _currentCooldown = 0f;
        
        public float CurrentChanceOfPowerAttack { get; private set; }
        
        public void Spawn()
        {
            if (StartingDifficulty == 0)
            {
                Destroy(gameObject);
                return;
            }
        
            CurrentRoom = StartingRoom;
            transform.position = StartingRoom.transform.position;

            EventHandler.Instance.OnPlayerSanityCrossing50 += OnPlayerSanityCrossing50;
            EventHandler.Instance.OnPlayerSanityCrossing25 += OnPlayerSanityCrossing25;
            EventHandler.Instance.OnPlayerSanityCrossing1 += OnPlayerSanityCrossing1;

            gameObject.SetActive(true);

            CurrentChanceOfPowerAttack = ChanceOnSanityAbove50;
            
            DebugHandler.Instance.SpawnedEnemies.Add(this);
        
            Debug.Log($"{gameObject.name}: I've spawned in {StartingRoom.name}.");
        }

        private void OnPlayerSanityCrossing50(bool isBelow)
        {
            CurrentChanceOfPowerAttack = isBelow ? ChanceOnSanityBelow50 : ChanceOnSanityAbove50;
        }
        
        private void OnPlayerSanityCrossing25(bool isBelow)
        {
            CurrentChanceOfPowerAttack = isBelow ? ChanceOnSanityBelow25 : ChanceOnSanityBelow50;
        }
        
        private void OnPlayerSanityCrossing1(bool isBelow)
        {
            CurrentChanceOfPowerAttack = isBelow ? ChanceOnSanityBelow1 : ChanceOnSanityBelow25;
        }
        
        protected void ChangeRoom(GameObject newRoom)
        {
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
    
        public void FailAttack()
        {
            IsAttacking = false;
            ChangeRoom(StartingRoom);
            Debug.Log($"{gameObject.name}: I've failed the attack on player. Teleporting to {StartingRoom.name}.");
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
            CurrentAttackPower += UnityEngine.Time.deltaTime * GameHandler.Instance.CurrentDangerLevel;

            if (CurrentAttackPower >= AttackPower)
            {
                SuccessfulAttack();
                return;
            }

            CurrentAttackingTime += UnityEngine.Time.deltaTime;

            if (CurrentAttackingTime >= AttackingTime)
            {
                var chance = Random.Range(0f, 100f);
                
                
                FailAttack();
            }
        }
        
        private void CheckMove()
        {
            _currentCooldown += UnityEngine.Time.deltaTime;

            if (_currentCooldown < MoveCooldown)
            {
                return;
            }

            _currentCooldown = 0f;
        
            var randomNumber = Random.Range(1, 20);

            if (StartingDifficulty <= randomNumber)
            {
                Debug.Log($"{gameObject.name}: I've failed my movement.");
                return;
            }

            Move();
        }

        protected abstract void Move();
    }
}