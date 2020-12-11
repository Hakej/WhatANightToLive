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
        
        [Header("Enemy attack's strength")]
        public float AttackPower;
        public float AttackingTime;

        protected GameObject CurrentRoom;

        private float _currentCooldown = 0f;
        private float _currentAttackPower = 0f;
        private float _currentAttackingTime = 0f;
        private bool _isAttacking = false;
        
        public void Spawn()
        {
            if (StartingDifficulty == 0)
            {
                Destroy(gameObject);
                return;
            }
        
            CurrentRoom = StartingRoom;
            transform.position = StartingRoom.transform.position;
            
            gameObject.SetActive(true);
        
            Debug.Log($"{gameObject.name}: I've spawned in {StartingRoom.name}.");
        }

        protected void ChangeRoom(GameObject newRoom)
        {
            CurrentRoom = newRoom;
            transform.position = newRoom.transform.position;
        }

        protected void ChangeToAttackMode()
        {
            _isAttacking = true;
        }

        private void SuccessfulAttack()
        {
            EventHandler.Instance.Lose();     
        }
    
        public void FailAttack()
        {
            _isAttacking = false;
            ChangeRoom(StartingRoom);
            Debug.Log($"{gameObject.name}: I've failed the attack on player. Teleporting to {StartingRoom.name}.");
        }
        
        private void Update()
        {
            if (_isAttacking)
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
            _currentAttackPower += UnityEngine.Time.deltaTime * GameHandler.Instance.CurrentDangerLevel;

            if (_currentAttackPower >= AttackPower)
            {
                SuccessfulAttack();
                return;
            }

            _currentAttackingTime += UnityEngine.Time.deltaTime;

            if (_currentAttackingTime >= AttackingTime)
            {
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