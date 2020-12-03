using System.Collections.Generic;
using Classes.Interfaces;
using Classes.Static;
using UnityEngine;

public class GameHandler : Singleton<GameHandler>
{
    public SceneLoader SceneLoader;
    
    [Header("Sanity")]
    public float StartingSanity = 100f;
    public float BaseSanityDrop = 0.05f;
    public static Sanity Sanity;

    [Header("Game time")]
    public int StartingHour = 22;
    public int StartingMinutes = 0;
    public int WinningHour = 6;
    public static GameTime GameTime;
    
    private readonly List<IUpdateable> _updateables = new List<IUpdateable>();

    private void Start()
    {
        Sanity = new Sanity(StartingSanity, BaseSanityDrop);
        GameTime = new GameTime(StartingHour, StartingMinutes);
        
        _updateables.Add(Sanity);
        _updateables.Add(GameTime);

        EventHandler.Instance.OnWin += OnWin;
        EventHandler.Instance.OnLose += OnLose;
    }

    private void Update()
    {
        foreach (var updateable in _updateables)
        {
            updateable.Update(Time.deltaTime);
        }
    }

    private void OnWin()
    {
        // TODO: Add winning logic
        SceneLoader.LoadWinScene();
    }

    private void OnLose()
    {
        // TODO: Add losing logic
        SceneLoader.LoadLoseScene();
    }
}
