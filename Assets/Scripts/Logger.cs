using Classes.Abstracts;
using GameObjects;
using UnityEngine;
using Singletons;

public class Logger : MonoBehaviour
{
    private void Start()
    {
        EventManager.Instance.OnEnemySpawn += OnEnemySpawn;
        EventManager.Instance.OnEnemyChangingRoom += OnEnemyMove;
    }

    private void OnEnemyMove(Enemy enemy, Room oldRoom, Room newRoom)
    {
        Debug.Log($"<{enemy.name}> has moved from [{oldRoom.name}] to [{newRoom.name}].");
    }

    private void OnEnemySpawn(Enemy enemy)
    {
        Debug.Log($"<{enemy.name}> has spawned in [{enemy.SpawnRoom.name}].");
    }
}