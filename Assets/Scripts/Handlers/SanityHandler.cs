using System;
using Classes.Interfaces;
using UnityEngine;
using EventHandler = Handlers.EventHandler;

namespace Handlers
{
    [Serializable]
    public class SanityHandler : Singleton<SanityHandler>
    {
        public float StartingSanity = 100f;
        public float SanityDropInDark = 1f;
        public float SanityGainInLight = 0.2f;

        [Header("On enemy door hit")]
        public float MinDoorHitLoss = 5f;
        public float MaxDoorHitLoss = 15f;

        public float CurrentSanity { get; private set; }

        public float CurrentSanitySense
        {
            get => CurrentSanity / StartingSanity;
            private set => CurrentSanitySense = value;
        }

        public void Start()
        {
            CurrentSanity = StartingSanity;

            EventHandler.Instance.OnDoorHit += OnDoorHit;
        }

        private void OnDoorHit(Door hitDoor)
        {
            CurrentSanity -= UnityEngine.Random.Range(MinDoorHitLoss, MaxDoorHitLoss);
        }

        public void Update()
        {
            if (GameHandler.Instance.IsPowerOn)
            {
                CurrentSanity += SanityGainInLight * Time.deltaTime;
            }
            else if (CurrentSanity > 0)
            {
                CurrentSanity -= SanityDropInDark * Time.deltaTime;
            }

            if (CurrentSanity > StartingSanity)
            {
                CurrentSanity = StartingSanity;
            }
            else if (CurrentSanity < 0)
            {
                CurrentSanity = 0;
            }
        }
    }
}