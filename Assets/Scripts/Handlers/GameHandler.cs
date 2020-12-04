using System.Collections.Generic;
using Classes.Interfaces;
using Classes.Static;
using UnityEditor;
using UnityEngine;
using Time = Classes.Time;

namespace Handlers
{
    public class GameHandler : Singleton<GameHandler>
    {
        [Header("Scenes")]
        public SceneHandler SceneHandler;
        public SceneAsset WinScene;
        public SceneAsset LoseScene;

        [Header("Sanity")]
        public float StartingSanity = 100f;
        public float BaseSanityDrop = 0.05f;
        public static Sanity Sanity;

        [Header("Game time")] 
        public Time StartingTime;
        public Time WinningTime;
        public static GameTime GameTime;
    
        private readonly List<IUpdateable> _updateables = new List<IUpdateable>();

        private void Start()
        {
            GameTime = new GameTime(StartingTime, WinningTime);
            Sanity = new Sanity(StartingSanity, BaseSanityDrop);
        
            _updateables.Add(Sanity);
            _updateables.Add(GameTime);

            EventHandler.Instance.OnWin += OnWin;
            EventHandler.Instance.OnLose += OnLose;
        }

        private void Update()
        {
            foreach (var updateable in _updateables)
            {
                updateable.Update(UnityEngine.Time.deltaTime);
            }
        }

        private void OnWin()
        {
            // TODO: Add winning logic
            SceneHandler.LoadScene(WinScene);
        }

        private void OnLose()
        {
            // TODO: Add losing logic
            SceneHandler.LoadScene(LoseScene);
        }
    }
}
