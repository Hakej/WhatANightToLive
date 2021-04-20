using Classes.Abstracts;
using GameObjects;
using Handlers;
using UnityEngine;
using What_a_Night_to_Live.Assets.Classes.Exceptions;
using Random = UnityEngine.Random;

public class GenericEnemy : Enemy
{
    [Header("Needed tags")]
    public string PlayerAdjacentRoomTag = "PlayerAdjacentRoom";
    public string PlayerAdjacentVentTag = "PlayerAdjacentVent";

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

        if (newRoom.CompareTag(PlayerAdjacentRoomTag) || newRoom.CompareTag(PlayerAdjacentVentTag))
        {
            ChangeToAttackMode();
        }
    }

    private Room GetNextRoom(bool isRoomBest)
    {
        Room nextRoom = null;
        Room[] adjRooms = CurrentRoom.AdjacentRooms;

        // Get first adjacent room 
        foreach (var room in adjRooms)
        {
            if (RoomsWeights.ContainsKey(room))
            {
                nextRoom = room;
                break;
            }
        }

        // If there is no adjacent room enemy can go to, throw an error
        if (nextRoom == null)
        {
            throw new NoNextRoomException(CurrentRoom);
        }

        for (int i = 1; i < adjRooms.Length; i++)
        {
            // If can't go to a room, ignore it
            if (!RoomsWeights.ContainsKey(adjRooms[i]))
            {
                continue;
            }

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
