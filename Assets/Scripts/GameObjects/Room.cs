using System.Collections.Generic;
using Classes.Abstracts;
using Controllers;
using Handlers;
using UnityEngine;

namespace GameObjects
{
    public class Room : MonoBehaviour
    {
        public Room[] AdjacentRooms;

        [Header("Audio Decoy")]
        public bool IsDecoyEnabled = false;
        public AudioDecoy AudioDecoy;

        [Header("Scanner")]
        public bool IsScanerEnabled = false;
        public Scanner Scanner;

        [HideInInspector]
        public List<Enemy> EnemiesInRoom = new List<Enemy>();

        private void Start()
        {
            if (!IsDecoyEnabled)
            {
                Destroy(AudioDecoy.gameObject);
            }

            if (!IsScanerEnabled)
            {
                Destroy(Scanner.gameObject);
            }

            EventHandler.Instance.OnEnemySpawn += OnEnemySpawn;
            EventHandler.Instance.OnEnemyChangingRoom += OnEnemyChangingRoom;
        }

        private void OnEnemySpawn(Enemy enemy)
        {
            if (enemy.SpawnRoom == this)
            {
                EnemiesInRoom.Add(enemy);
            }
        }

        private void OnEnemyChangingRoom(Enemy enemy, Room oldRoom, Room newRoom)
        {
            if (oldRoom == this)
            {
                if (EnemiesInRoom.Contains(enemy))
                {
                    EnemiesInRoom.Remove(enemy);
                }
            }
            else if (newRoom == this)
            {
                if (!EnemiesInRoom.Contains(enemy))
                {
                    EnemiesInRoom.Add(enemy);
                }
            }
        }
    }
}
