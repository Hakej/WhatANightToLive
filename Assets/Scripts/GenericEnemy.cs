using Classes.Abstracts;
using Classes.Static;
using GameObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenericEnemy : Enemy
{
    protected override void Move()
    {
        Room newRoom;
        var sanity = SanityHandler.Instance;
        var smartMoveChance = Random.Range(0f, 1f);
        var currentSmartMoveChance = Mathf.Lerp(MaxSmartMoveChance, BaseSmartMoveChance, sanity.CurrentSanity / sanity.StartingSanity);

        var isNextRoomTheBest = smartMoveChance <= currentSmartMoveChance;
        newRoom = GetNextBestRoom(CurrentRoom.AdjacentRooms, isNextRoomTheBest);

        if (isNextRoomTheBest)
        {
            Debug.Log("That was the best move.");
        }
        else
        {
            Debug.Log("That was the worst move.");
        }

        ChangeRoom(newRoom);

        if (newRoom.CompareTag("PlayerAdjacentRoom"))
        {
            ChangeToAttackMode();
        }
    }

    private Room GetNextBestRoom(Room[] rooms, bool isBest)
    {
        Room nextRoom = rooms[0];

        for (int i = 1; i < rooms.Length; i++)
        {
            if (rooms[i].Weight == nextRoom.Weight)
            {
                var rn = Random.Range(0, 1);

                if (rn == 1)
                {
                    nextRoom = rooms[i];
                }
            }
            else if (isBest)
            {
                if (rooms[i].Weight < nextRoom.Weight)
                {
                    nextRoom = rooms[i];
                }
            }
            else
            {
                if (rooms[i].Weight > nextRoom.Weight)
                {
                    nextRoom = rooms[i];
                }
            }
        }

        return nextRoom;
    }
}
