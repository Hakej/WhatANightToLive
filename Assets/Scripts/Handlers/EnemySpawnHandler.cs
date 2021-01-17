using System.Collections.Generic;
using System.Linq;
using Classes.Abstracts;
using UnityEngine;

namespace Handlers
{
    public class EnemySpawnHandler : MonoBehaviour
    {
        public bool DisableEnemies;
        
        public List<Enemy> EnemiesToSpawn;
    
        private void Start()
        {
            if (!DisableEnemies)
            {
                return;
            }

            Destroy(this);
        }

        private void Update()
        {
            var gameTime = GameHandler.Instance.GameTimeHandler.CurrentGameTime;
            
            foreach (var enemy in EnemiesToSpawn.Reverse<Enemy>())
            {
                if (gameTime.IsEqual(enemy.SpawningGameTime))
                {
                    enemy.Spawn();
                    EnemiesToSpawn.Remove(enemy);
                }
            }
        }
    }
}
