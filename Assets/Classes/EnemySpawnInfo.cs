using System;
using Classes;
using GameObjects;
using UnityEngine;

[Serializable]
public class EnemySpawnInfo
{
    public GameObject EnemyPrefab;
    public Room SpawnRoom;
    public GameTime SpawnTime;
}