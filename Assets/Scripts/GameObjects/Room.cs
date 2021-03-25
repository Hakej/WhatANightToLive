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

        private void Start()
        {
            if (!IsDecoyEnabled)
            {
                Destroy(AudioDecoy.gameObject);
            }
        }
    }
}
