using System.Collections.Generic;
using System.Linq;
using Classes.Abstracts;
using GameObjects;
using Handlers;
using UnityEngine;

public class DebugMinimap : MonoBehaviour
{
    public Room ReferencedRoom;

    [Header("Enemy icons")]
    public float MultipleEnemyIconsOffset = 4f;

    public Dictionary<Enemy, GameObject> EnemyIcons = new Dictionary<Enemy, GameObject>();

    private void Start()
    {
        EventHandler.Instance.OnEnemySpawn += OnEnemySpawn;
        EventHandler.Instance.OnEnemyChangingRoom += OnEnemyChangingRoom;
    }

    private void OnEnemySpawn(Enemy enemy)
    {
        if (enemy.SpawnRoom == ReferencedRoom)
        {
            AddIcon(enemy);
        }
    }

    private void OnEnemyChangingRoom(Enemy enemy, Room oldRoom, Room newRoom)
    {
        if (oldRoom == ReferencedRoom)
        {
            RemoveIcon(enemy);
        }
        else if (newRoom == ReferencedRoom)
        {
            AddIcon(enemy);
        }
    }

    private void AddIcon(Enemy enemy)
    {
        var enemyIconObject = Instantiate(enemy.MinimapIcon, transform);
        enemyIconObject.name = enemy.name + enemyIconObject.name.Replace("(Clone)", "");

        EnemyIcons.Add(enemy, enemyIconObject);

        for (var i = 0; i < EnemyIcons.Count; i++)
        {
            var iconObject = EnemyIcons.ElementAt(i).Value;
            iconObject.transform.Translate(Vector3.right * i * MultipleEnemyIconsOffset);
        }
    }

    private void RemoveIcon(Enemy enemy)
    {
        if (EnemyIcons.TryGetValue(enemy, out var enemyIcon))
        {
            EnemyIcons.Remove(enemy);
            Destroy(enemyIcon);
        }

        for (var i = 0; i < EnemyIcons.Count; i++)
        {
            var iconObject = EnemyIcons.ElementAt(i).Value;
            iconObject.transform.Translate(Vector3.right * i * MultipleEnemyIconsOffset);
        }
    }
}
