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
    public float EnemyIconsScale = 50f;
    public float MultipleEnemyIconsOffset = 4f;

    public Dictionary<Enemy, GameObject> EnemyIcons = new Dictionary<Enemy, GameObject>();

    private void Start()
    {
        EventHandler.Instance.OnEnemyChangingRoom += OnEnemyChangingRoom;
        EventHandler.Instance.OnEnemySpawn += OnEnemySpawn;
    }

    private void OnEnemySpawn(Enemy enemy)
    {
        if (enemy.SpawnRoom == ReferencedRoom)
        {
            var enemyIcon = new GameObject();
            var spriteRenderer = enemyIcon.AddComponent<SpriteRenderer>();

            spriteRenderer.sprite = enemy.EnemyMinimapIcon;

            var enemyIconObject = Instantiate(enemyIcon, transform);
            enemyIconObject.transform.localScale = Vector3.one * EnemyIconsScale;

            EnemyIcons.Add(enemy, enemyIconObject);

            for (var i = 0; i < EnemyIcons.Count; i++)
            {
                var iconObject = EnemyIcons.ElementAt(i).Value;
                iconObject.transform.Translate(Vector3.right * i * MultipleEnemyIconsOffset);
            }
        }
    }

    private void OnEnemyChangingRoom(Enemy enemy, Room oldRoom, Room newRoom)
    {
        if (oldRoom == ReferencedRoom)
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
        else if (newRoom == ReferencedRoom)
        {
            var enemyIcon = new GameObject();
            var spriteRenderer = enemyIcon.AddComponent<SpriteRenderer>();

            spriteRenderer.sprite = enemy.EnemyMinimapIcon;

            var enemyIconObject = Instantiate(enemyIcon, transform);
            enemyIconObject.transform.localScale = Vector3.one * EnemyIconsScale;

            EnemyIcons.Add(enemy, enemyIconObject);

            for (var i = 0; i < EnemyIcons.Count; i++)
            {
                var iconObject = EnemyIcons.ElementAt(i).Value;
                iconObject.transform.Translate(Vector3.right * i * MultipleEnemyIconsOffset);
            }
        }

    }
}
