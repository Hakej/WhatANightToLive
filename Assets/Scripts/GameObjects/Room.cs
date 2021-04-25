using System.Collections.Generic;
using Classes.Abstracts;
using Controllers;
using Handlers;
using UnityEngine;

namespace GameObjects
{
    public class Room : MonoBehaviour
    {
        public bool IsVent = false;
        public Room[] AdjacentRooms;

        [Header("Audio Decoy")]
        public bool IsDecoyEnabled = false;
        public AudioDecoy AudioDecoy;

        [Header("Scanner")]
        public bool IsScannerEnabled = false;
        public Scanner Scanner;

        [Header("Door")]
        public bool IsDoorEnabled = false;
        public Door Door;

        [HideInInspector]
        public List<Enemy> EnemiesInRoom = new List<Enemy>();

        private void Start()
        {
            if (!IsDecoyEnabled)
            {
                Destroy(AudioDecoy.gameObject);
                Destroy(AudioDecoy);
            }

            if (!IsScannerEnabled)
            {
                Destroy(Scanner.gameObject);
                Destroy(Scanner);
            }

            if (!IsDoorEnabled)
            {
                Destroy(Door.gameObject);
                Destroy(Door);
            }

            EventHandler.Instance.OnEnemySpawn += OnEnemySpawn;
            EventHandler.Instance.OnEnemyChangingRoom += OnEnemyChangingRoom;
        }

        private void OnForceLose()
        {
            if (Door != null && !Door.IsClosed)
            {

            }
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
