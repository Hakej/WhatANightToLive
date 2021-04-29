using System.Collections.Generic;
using System.Text;
using Classes.Abstracts;
using Handlers;
using TMPro;
using UnityEngine;
using Singletons;

public class DebugHandler : MonoBehaviour
{
    [Header("Configuration")]
    public bool IsDebugModeOn = true;

    [Header("UIs")]
    public Canvas DebugUICanvas;
    public TextMeshProUGUI PlayerInfoUI;
    public TextMeshProUGUI EnemyInfoUI;

    [Header("Computer UIs")]
    public GameObject DebugMinimap;

    [Header("Needed handlers")]
    public GameHandler GameHandler;
    public PowerHandler PowerHandler;
    public SanityHandler SanityHandler;

    private List<Enemy> _spawnedEnemies = new List<Enemy>();

    private void Start()
    {
        UpdateDebug();

        EventManager.Instance.OnLose += OnLose;
        EventManager.Instance.OnEnemySpawn += OnEnemySpawn;
    }

    public void Update()
    {
        if (!IsDebugModeOn)
        {
            return;
        }

        var gh = GameHandler;
        var ph = PowerHandler;
        var sh = SanityHandler;

        var playerInfo = new StringBuilder();
        Color playerInfoColor;

        playerInfo.Append($"Current sanity level: {sh.CurrentSanity}\n");

        if (ph.IsPowerOn)
        {
            playerInfo.Append($"Current sanity gain: {sh.SanityGainInLight}\n");
            playerInfoColor = Color.white;
        }
        else
        {
            playerInfo.Append($"Current sanity drop: {sh.SanityDropInDark}\n");
            playerInfoColor = Color.yellow;
        }

        PlayerInfoUI.text = playerInfo.ToString();
        PlayerInfoUI.color = playerInfoColor;

        var enemies = new StringBuilder();

        var isAnyoneAttacking = false;

        foreach (var enemy in _spawnedEnemies)
        {
            enemies.Append($"{enemy.gameObject.name}\n");
            enemies.Append($"Current difficulty: {enemy.CurrentDifficulty}\n");
            enemies.Append($"Current room: {enemy.CurrentRoom.name}\n");
            enemies.Append($"Is attacking: {enemy.IsAttacking}\n");

            if (enemy.IsAttacking)
            {
                enemies.Append($"Current attack power: {enemy.CurrentAttackPower.ToString("0.00")}\n");
                enemies.Append($"Attacking time left: {(enemy.AttackingTime - enemy.CurrentAttackingTime).ToString("0.00")}\n");

                isAnyoneAttacking = true;
            }
            else
            {
                enemies.Append($"Current move cooldown: {enemy.CurrentMoveCooldown.ToString("0.00")}\n");
            }

            enemies.Append("\n");
        }

        EnemyInfoUI.text = enemies.ToString();

        if (isAnyoneAttacking)
        {
            EnemyInfoUI.color = new Color(255, 0, 0);
        }
        else
        {
            EnemyInfoUI.color = new Color(255, 255, 255);
        }
    }

    public void ToggleDebugMode()
    {
        IsDebugModeOn = !IsDebugModeOn;
        UpdateDebug();
    }

    private void UpdateDebug()
    {
        DebugMinimap.SetActive(IsDebugModeOn);
        DebugUICanvas.gameObject.SetActive(IsDebugModeOn);
    }

    private void OnLose()
    {
        Destroy(this);
    }

    private void OnEnemySpawn(Enemy enemy)
    {
        _spawnedEnemies.Add(enemy);
    }
}
