using System.Collections.Generic;
using System.Text;
using Classes.Abstracts;
using TMPro;
using UnityEngine;

namespace Handlers
{
    public class DebugHandler : Singleton<DebugHandler>
    {
        public bool IsDebugModeOn = true;
        public Canvas DebugUICanvas;
        public TextMeshProUGUI PlayerInfo;
        public TextMeshProUGUI EnemyInfo;
        public List<Enemy> SpawnedEnemies;

        private void Start()
        {
            SpawnedEnemies = new List<Enemy>();
            
            UpdateDebug();
        }

        public void Update()
        {
            if (!IsDebugModeOn)
            {
                return;
            }

            var gh = GameHandler.Instance;
        
            PlayerInfo.text = $"Current sanity level: {gh.Sanity.CurrentSanity}\n" +
                             $"Current fear level: {gh.Sanity.CurrentFearLevel}\n" +
                             $"Current sanity drop: {gh.Sanity.CurrentSanityDrop}\n" +
                             $"Current danger level: {gh.CurrentDangerLevel}";
            
            var enemies = new StringBuilder();

            foreach (var enemy in SpawnedEnemies)
            {
                enemies.Append($"{enemy.gameObject.name}\n");
                enemies.Append($"Current room: {enemy.CurrentRoom.name}\n");
                enemies.Append($"Chance of power attack: {enemy.CurrentChanceOfPowerAttack}\n");
                enemies.Append($"Is attacking: {enemy.IsAttacking}\n");
                
                if (enemy.IsAttacking)
                {
                    enemies.Append($"Current attack power: {enemy.CurrentAttackPower}\n");
                    enemies.Append($"Current attacking time: {enemy.CurrentAttackingTime}\n");
                }

                enemies.Append("\n");
            }

            EnemyInfo.text = enemies.ToString();
        }

        public void ToggleDebugMode()
        {
            IsDebugModeOn = !IsDebugModeOn;
            UpdateDebug();
        }

        private void UpdateDebug()
        {
            DebugUICanvas.gameObject.SetActive(IsDebugModeOn);
        }
    }
}