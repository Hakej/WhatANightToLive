using System.Collections.Generic;
using Classes.Interfaces;
using Classes.Static;
using UnityEngine;

public class GameHandler : Singleton<GameHandler>
{
    [Header("Sanity")]
    public static Sanity Sanity;
    public float StartingSanity = 100f;
    public float BaseSanityDrop = 0.05f;

    [Header("Game time")]
    public static GameTime GameTime;
    public int StartingHour;
    public int StartingMinutes;
    
    private List<IUpdateable> Updateables = new List<IUpdateable>();

    private void Start()
    {
        Sanity = new Sanity(StartingSanity, BaseSanityDrop);
        GameTime = new GameTime(StartingHour, StartingMinutes);
        
        Updateables.Add(Sanity);
        Updateables.Add(GameTime);
    }

    private void Update()
    {
        foreach (var updateable in Updateables)
        {
            updateable.Update(Time.deltaTime);
        }
    }
}
