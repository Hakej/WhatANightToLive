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
            var gameTime = GameTimeHandler.Instance.CurrentGameTime;

            foreach (var enemySpawnInfo in EnemiesToSpawn.Reverse<EnemySpawnInfo>())
            {
                if (gameTime.IsGreaterOrEqual(enemySpawnInfo.SpawnTime))
                {
                    SpawnEnemy(enemySpawnInfo);
                    EnemiesToSpawn.Remove(enemySpawnInfo);
                }
            }
        }

        private void SpawnEnemy(EnemySpawnInfo info)
        {
            var spawnedEnemy = Instantiate(info.EnemyPrefab, EnemiesContainer.transform);
            spawnedEnemy.name = info.EnemyPrefab.name;

            var spawnedEnemyScript = spawnedEnemy.GetComponent<Enemy>();
            spawnedEnemyScript.Spawn(info);

            EventHandler.Instance.EnemySpawn(spawnedEnemyScript);
        }
    }
}
