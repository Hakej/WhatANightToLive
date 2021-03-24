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
        var smartMoveChance = Random.Range(0f, 1f);
        var currentSanitySense = SanityHandler.Instance.CurrentSanitySense;
        var currentSmartMoveChance = Mathf.Lerp(MaxSmartMoveChance, MinSmartMoveChance, currentSanitySense);

        var isNextRoomTheBest = smartMoveChance <= currentSmartMoveChance;
        newRoom = GetNextRoom(isNextRoomTheBest || IsCurrentlyFooled);

        ChangeRoom(newRoom);

        if (isNextRoomTheBest && !IsCurrentlyFooled)
        {
            Debug.Log("That was the best move.");
        }
        else if (!isNextRoomTheBest && !IsCurrentlyFooled)
        {
            Debug.Log("That was a bad move.");
        }
        else
        {
            Debug.Log("I was fooled into bad move.");
        }

        if (newRoom.CompareTag("PlayerAdjacentRoom"))
        {
            ChangeToAttackMode();
        }
    }

    private Room GetNextRoom(bool isRoomBest)
    {
        var adjRooms = CurrentRoom.AdjacentRooms;
        var nextRoom = adjRooms[0];
        var nextRoomWeight = RoomsWeights[nextRoom];

        for (int i = 1; i < adjRooms.Length; i++)
        {
            if (RoomsWeights[adjRooms[i]] == RoomsWeights[nextRoom])
            {
                var rn = Random.Range(0, 2);

                if (rn == 1)
                {
                    nextRoom = adjRooms[i];
                }
            }
            else if (isRoomBest)
            {
                if (RoomsWeights[adjRooms[i]] < RoomsWeights[nextRoom])
                {
                    nextRoom = adjRooms[i];
                }
            }
            else
            {
                if (RoomsWeights[adjRooms[i]] > RoomsWeights[nextRoom])
                {
                    nextRoom = adjRooms[i];
                }
            }
        }

        return nextRoom;
    }
}
