using System.Collections.Generic;
using System.Linq;
using Classes.Abstracts;
using UnityEngine;

namespace Handlers
{
    public class EnemySpawnHandler : MonoBehaviour
    {
        public bool DisableEnemies;
        public GameObject EnemiesGameObject;
        
        private List<Enemy> _enemiesToSpawn;
    
        private void Start()
        {
            if (DisableEnemies)
            {
                Destroy(this);
                return;
            }
            
            _enemiesToSpawn = new List<Enemy>();
        
            foreach (Transform child in EnemiesGameObject.transform)
            {
                _enemiesToSpawn.Add(child.GetComponent<Enemy>()); 
            }
        }

        private void Update()
        {
            var gameTime = GameHandler.Instance.GameTime;
            
            foreach (var enemy in _enemiesToSpawn.Reverse<Enemy>())
            {
                if (gameTime.IsCurrentTimeEqual(enemy.SpawningTime))
                {
                    enemy.Spawn();
                    _enemiesToSpawn.Remove(enemy);
                }
            }
        }
    }
}
