using System.Collections.Generic;
using System.Linq;
using Classes.Abstracts;
using UnityEngine;

namespace Handlers
{
    public class EnemySpawnHandler : MonoBehaviour
    {
        public bool DisableEnemies;

        public GameObject EnemiesContainer;

        public List<EnemySpawnInfo> EnemiesToSpawn;

        private void Start()
        {
            if (DisableEnemies)
            {
                Destroy(this);
                return;
            }

            InvokeRepeating("UpdateEverySecond", 0f, 1.0f);
        }

        private void UpdateEverySecond()
        {
            var gameTime = GameHandler.Instance.GameTimeHandler.CurrentGameTime;

            foreach (var enemySpawnInfo in EnemiesToSpawn.Reverse<EnemySpawnInfo>())
            {
                var info = enemySpawnInfo;
                if (gameTime.IsEqual(info.SpawnTime))
                {
                    var spawnedEnemy = Instantiate(info.EnemyPrefab, EnemiesContainer.transform);
                    spawnedEnemy.name = info.EnemyPrefab.name;

                    var spawnedEnemyScript = spawnedEnemy.GetComponent<Enemy>();
                    spawnedEnemyScript.Spawn(enemySpawnInfo);

                    EnemiesToSpawn.Remove(enemySpawnInfo);
                    EventHandler.Instance.EnemySpawn(spawnedEnemyScript);
                }
            }
        }
    }
}
