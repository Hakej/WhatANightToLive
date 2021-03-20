using System.Collections.Generic;
using GameObjects;
using UnityEngine;

public class RoomsHandler : MonoBehaviour
{
    public Room Destination;

    private void Start()
    {
        CalculateWeights(Destination, null, 0);
    }

    private void CalculateWeights(Room room, Room previous, int weight)
    {
        if (room.Weight <= weight)
            return;

        room.Weight = weight;

        foreach (var adjRoom in room.AdjacentRooms)
        {
            if (adjRoom == previous)
                continue;

            CalculateWeights(adjRoom, room, weight + 1);
        }
    }
}