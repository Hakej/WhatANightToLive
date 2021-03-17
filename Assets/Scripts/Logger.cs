using Classes.Abstracts;
using UnityEngine;
using EventHandler = Handlers.EventHandler;

public class Logger : Singleton<Logger>
{
    private void Start()
    {
        EventHandler.Instance.OnEnemySpawn += OnEnemySpawn;
    }

    private void OnEnemySpawn(Enemy enemy)
    {
        Debug.Log($"{enemy.name} has spawned in {enemy.SpawnRoom.name}.");
    }
}