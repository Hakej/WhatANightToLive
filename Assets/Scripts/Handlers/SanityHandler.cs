using System;
using Classes.Interfaces;
using UnityEngine;
using EventHandler = Handlers.EventHandler;

namespace Handlers
{
    [Serializable]
    public class SanityHandler : Singleton<SanityHandler>
    {
        public float BaseSanityDrop = 0.3f;
        public float StartingSanity = 100f;

        public int CurrentFearLevel = 0;

        public float CurrentSanity { get; private set; }
        public float CurrentSanityDrop { get; private set; }

        public float CurrentSanitySense
        {
            get => CurrentSanity / StartingSanity;
            private set => CurrentSanitySense = value;
        }

        [Header("Sanity Gain When Safe")]
        public float SanityGain = 0.5f;
        public bool IsGainingSanity = false;

        public void Start()
        {
            CurrentSanity = StartingSanity;
        }

        public void Update()
        {
            var oldSanity = CurrentSanity;

            CurrentSanityDrop = BaseSanityDrop * CurrentFearLevel;

            if (CurrentFearLevel == 0)
            {
                CurrentSanity += SanityGain * Time.deltaTime;
            }
            else if (CurrentSanity > 0f)
            {
                CurrentSanity -= CurrentSanityDrop * Time.deltaTime;
            }

            if (CurrentSanity > StartingSanity)
            {
                CurrentSanity = StartingSanity;
            }
        }
    }
}