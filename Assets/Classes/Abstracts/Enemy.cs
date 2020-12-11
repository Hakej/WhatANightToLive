using Handlers;
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
    
        protected float CurrentCooldown = 0f;
        protected GameObject CurrentRoom;
        
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
        
        public void ChangeRoom(GameObject newRoom)
        {
            CurrentRoom = newRoom;
            transform.position = newRoom.transform.position;
        }
        
        public void TryAttack()
        {
            if (GameHandler.Instance.IsPlayersPowerOn)
            {       
                SuccessfulAttack();
            }
            else
            {
                FailAttack();
            }
        }

        public void SuccessfulAttack()
        {
            EventHandler.Instance.Lose();     
        }
    
        public void FailAttack()
        {
            ChangeRoom(StartingRoom);
            Debug.Log($"{gameObject.name}: I've failed the attack on player. Teleporting to {StartingRoom.name}.");
        }
        
        private void Update()
        {
            CurrentCooldown += UnityEngine.Time.deltaTime;

            if (CurrentCooldown < MoveCooldown)
            {
                return;
            }

            CurrentCooldown = 0f;
        
            var randomNumber = Random.Range(1, 20);

            if (StartingDifficulty <= randomNumber)
            {
                Debug.Log($"{gameObject.name}: I've failed my movement.");
                return;
            }

            Move();
        }
        
        public abstract void Move();

    }
}