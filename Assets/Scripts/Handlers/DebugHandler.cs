using System.Collections.Generic;
using System.Text;
using Classes.Abstracts;
using TMPro;
using UnityEditor.UIElements;
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

            if (IsDebugModeOn)
            {     
                MinimapCamera.cullingMask |= 1 << LayerMask.NameToLayer(DebugMinimapLayer);
            }
            else
            {          
                MinimapCamera.cullingMask &=  ~(1 << LayerMask.NameToLayer(DebugMinimapLayer));
            }
        }

        private void OnEnemySpawn(Enemy enemy)
        {
            SpawnedEnemies.Add(enemy);
        }
    }
}