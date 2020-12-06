﻿using System.Collections.Generic;
using Classes.Interfaces;
using Classes.Static;
using UnityEditor;
using UnityEngine;
namespace Handlers
{
    public class GameHandler : Singleton<GameHandler>
    {
        [Header("Scenes")]
        public SceneHandler SceneHandler;
        public SceneAsset WinScene;
        public SceneAsset LoseScene;

        [Header("Sanity")]
        public Sanity Sanity;
        
        [Header("Game time")] 
        public GameTime GameTime;

        [Header("Player's Room's Power")]
        public bool IsPlayersPowerOn = true;
        public GameObject PlayerPowerSwitch;
        
        private readonly List<IUpdateable> _updateables = new List<IUpdateable>();

        private void Start()
        {
            _updateables.Add(Sanity);
            _updateables.Add(GameTime);

            foreach (var updateable in _updateables)
            {
                updateable.Start();
            }
            
            EventHandler.Instance.OnWin += OnWin;
            EventHandler.Instance.OnLose += OnLose;
            EventHandler.Instance.OnPowerToggle += OnPowerToggle;
        }

        private void Update()
        {
            foreach (var updateable in _updateables)
            {
                updateable.Update(Time.deltaTime);
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
        
        private void OnPowerToggle(bool areLightsOn, string poweredTag)
        {
            if (!PlayerPowerSwitch.CompareTag(poweredTag))
            {
                return;
            }
            
            IsPlayersPowerOn = areLightsOn;

            if (areLightsOn)
            {
                Sanity.CurrentFearLevel--;
            }
            else
            {
                Sanity.CurrentFearLevel++;
            }
        }
    }
}
