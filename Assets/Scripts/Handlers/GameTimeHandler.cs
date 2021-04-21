using System;
using Classes;
using UnityEngine;

namespace Handlers
{
    public class GameTimeHandler : Singleton<GameTimeHandler>
    {
        public GameTime StartingGameTime;

        [HideInInspector]
        public GameTime CurrentGameTime;

        private void Start()
        {
            CurrentGameTime = StartingGameTime;
        }

        public void Update()
        {
            CurrentGameTime.AddMinutes(Time.deltaTime);
        }
    }
}