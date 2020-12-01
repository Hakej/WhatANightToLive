using System.Collections.Generic;
using Classes.Interfaces;
using Classes.Static;
using UnityEngine;

public class GameHandler : Singleton<GameHandler>
{
    [Header("Sanity")]
    public float StartingSanity = 100f;
    public float BaseSanityDrop = 0.05f;
    public static Sanity Sanity;

    [Header("Game time")]
    public int StartingHour = 22;
    public int StartingMinutes = 0;
    public int WinningHour = 6;
    public static GameTime GameTime;
    
    private List<IUpdateable> Updateables = new List<IUpdateable>();

    private void Start()
    {
        Sanity = new Sanity(StartingSanity, BaseSanityDrop);
        GameTime = new GameTime(StartingHour, StartingMinutes);
        
        Updateables.Add(Sanity);
        Updateables.Add(GameTime);

        EventHandler.Instance.OnWin += OnWin;
    }

    private void Update()
    {
        foreach (var updateable in Updateables)
        {
            updateable.Update(Time.deltaTime);
        }
    }

    private static void OnWin()
    {
        // TODO: Add winning logic
        Debug.Log("You win!");
        
        Destroy(Instance.gameObject);
    }
}
