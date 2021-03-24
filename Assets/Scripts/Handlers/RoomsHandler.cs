using System.Collections.Generic;
using GameObjects;
using UnityEngine;

public class RoomsHandler : Singleton<RoomsHandler>
{
    public Room PlayerRoom;

    public Dictionary<Room, int> CalculateWeights(Room destination)
    {
        var weightedRooms = new Dictionary<Room, int>();

        return CalculateWeights(weightedRooms, destination, null);
    }

    private Dictionary<Room, int> CalculateWeights(Dictionary<Room, int> weightedRooms, Room currentRoom, Room previousRoom, int currentWeight = 0)
    {
        // Check if current room doesn't already have a weight, and if it does - check if it is not smaller or equal already.
        // This is to avoid situation, where AI has more than one path to reach its destination, and prefer the worse one,
        // because it was overcalculated by the longer path.
        if (weightedRooms.TryGetValue(currentRoom, out int currentRoomWeight))
        {
            if (currentRoomWeight <= currentWeight)
                return weightedRooms;
            else
                weightedRooms[currentRoom] = currentWeight;
        }
        else
        {
            weightedRooms.Add(currentRoom, currentWeight);
        }

        foreach (var adjRoom in currentRoom.AdjacentRooms)
        {
            if (adjRoom == previousRoom)
                continue;

            weightedRooms = CalculateWeights(weightedRooms, adjRoom, currentRoom, currentWeight + 1);
        }

        return weightedRooms;
    }
}