using System;
using Classes.Interfaces;
using UnityEngine;
using EventHandler = Handlers.EventHandler;

namespace Classes.Static
{
    [Serializable]
    public class SanityHandler : Singleton<SanityHandler>
    {
        public float BaseSanityDrop = 0.3f;
        public float StartingSanity = 100f;

        public int CurrentFearLevel = 0;

        public float CurrentSanity { get; private set; }
        public float CurrentSanityDrop { get; private set; }

        public void Start()
        {
            CurrentSanity = StartingSanity;
        }

        public void Update()
        {
            var oldSanity = CurrentSanity;

            CurrentSanityDrop = BaseSanityDrop + BaseSanityDrop * CurrentFearLevel;

            if (CurrentSanity > 0f)
            {
                CurrentSanity -= CurrentSanityDrop * Time.deltaTime;
            }

            if (CurrentSanity > StartingSanity)
            {
                CurrentSanity = StartingSanity;
            }

            if (oldSanity > 50f && CurrentSanity > 50f)
            {
                return;
            }

            if (oldSanity >= 50f && CurrentSanity < 50f)
            {
                EventHandler.Instance.PlayerSanityCrossing50(true);
            }
            else if (oldSanity < 50f && CurrentSanity >= 50f)
            {
                EventHandler.Instance.PlayerSanityCrossing50(false);
            }

            if (oldSanity >= 25f && CurrentSanity < 25f)
            {
                EventHandler.Instance.PlayerSanityCrossing25(true);
            }
            else if (oldSanity < 25f && CurrentSanity >= 25f)
            {
                EventHandler.Instance.PlayerSanityCrossing25(false);
            }

            if (oldSanity >= 1f && CurrentSanity < 1f)
            {
                EventHandler.Instance.PlayerSanityCrossing1(true);
            }
            else if (oldSanity < 1f && CurrentSanity >= 1f)
            {
                EventHandler.Instance.PlayerSanityCrossing1(false);
            }
        }

        public void GainSanity(float gainedSanity)
        {
            CurrentSanity += gainedSanity;
        }
    }
}