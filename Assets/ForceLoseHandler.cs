using System.Linq;
using Classes;
using Classes.Abstracts;
using GameObjects;
using Handlers;
using UnityEngine;

public class ForceLoseHandler : MonoBehaviour
{
    public GameTime ForceLoseGameTime;

    public Room LeftAttackingRoom;
    public Room RightAttackingRoom;

    private void Start()
    {
        InvokeRepeating("UpdateEverySecond", 0f, 1.0f);
    }

    public void UpdateEverySecond()
    {
        var gth = GameTimeHandler.Instance;

        if (gth.CurrentGameTime.IsEqual(ForceLoseGameTime))
        {
            ForceLose();
        }
    }

    private void ForceLose()
    {
        var attackingRoom = LeftAttackingRoom;

        var door = LeftAttackingRoom.Door;

        if (door != null && door.IsClosed)
        {
            attackingRoom = RightAttackingRoom;
        }

        // Get an enemy that ignores vents
        var attacker = FindObjectsOfType<Enemy>().Where(e => e.IgnoreVents).First();

        attacker.ChangeRoom(attackingRoom);
        attacker.SuccessfulAttack();
    }
}
