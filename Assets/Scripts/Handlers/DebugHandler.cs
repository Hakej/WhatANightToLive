using System.Collections.Generic;
using System.Text;
using Classes.Abstracts;
using TMPro;
using UnityEngine;

namespace Handlers
{
    public class DebugHandler : Singleton<DebugHandler>
    {
        [Header("Configuration")]
        public bool IsDebugModeOn = true;
        public Canvas DebugUICanvas;

        [Header("Infos")]
        public TextMeshProUGUI PlayerInfo;
        public TextMeshProUGUI EnemyInfo;

        [HideInInspector]
        public List<Enemy> SpawnedEnemies;

        [Header("Debug on minimap")]
        public Camera MinimapCamera;

        public string DebugMinimapLayer;

        private void Start()
        {
            EventHandler.Instance.OnEnemySpawn += OnEnemySpawn;

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

            PlayerInfo.text = $"Current sanity level: {gh.SanityHandler.CurrentSanity}\n" +
                             $"Current fear level: {gh.SanityHandler.CurrentFearLevel}\n" +
                             $"Current sanity drop: {gh.SanityHandler.CurrentSanityDrop}\n" +
                             $"Current danger level: {gh.CurrentDangerLevel}";

            var enemies = new StringBuilder();

            var isAnyoneAttacking = false;

            foreach (var enemy in SpawnedEnemies)
            {
                enemies.Append($"{enemy.gameObject.name}\n");
                enemies.Append($"Current room: {enemy.CurrentRoom.name}\n");
                enemies.Append($"Is attacking: {enemy.IsAttacking}\n");

                if (enemy.IsAttacking)
                {
                    enemies.Append($"Current attack power: {enemy.CurrentAttackPower.ToString("0.00")}\n");
                    enemies.Append($"Attacking time left: {(enemy.AttackingTime - enemy.CurrentAttackingTime).ToString("0.00")}\n");

                    isAnyoneAttacking = true;
                }
                else
                {
                    enemies.Append($"Current move cooldown: {enemy.CurrentMoveCooldown.ToString("0.00")}\n");
                }

                enemies.Append("\n");
            }

            EnemyInfo.text = enemies.ToString();

            if (isAnyoneAttacking)
            {
                EnemyInfo.color = new Color(255, 0, 0);
            }
            else
            {
                EnemyInfo.color = new Color(255, 255, 255);
            }
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

        private void OnEnemySpawn(Enemy enemy)
        {
            SpawnedEnemies.Add(enemy);
        }
    }
}