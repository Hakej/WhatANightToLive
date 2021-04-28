using System.Collections.Generic;
using GameObjects;
using UnityEngine;

public class RoomsHandler : MonoBehaviour
{
    public Room PlayerRoom;

    [Header("Needed tags")]
    [TagSelector]
    public string AdjacentRoomTag = "";
    [TagSelector]
    public string NextToAdjacentRoomTag = "";

    public Dictionary<Room, int> CalculateWeights(Room destination, bool ignoreVents, bool ignoreAdjRooms)
    {
        var weightedRooms = new Dictionary<Room, int>();

        return CalculateWeights(weightedRooms, destination, null, ignoreVents, ignoreAdjRooms);
    }

    private Dictionary<Room, int> CalculateWeights(Dictionary<Room, int> weightedRooms, Room currentRoom, Room previousRoom, bool ignoreVents, bool ignoreAdjRooms, int currentWeight = 0)
    {
        // Check if current room doesn't already have a weight, and if it does - check if it is not smaller or equal already.
        // This is to avoid situation, where AI has more than one path to reach its destination, and prefer the worse one,
        // because the better was overcalculated by the longer path.
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

            // Ignore vents so enemy never enters them
            if (ignoreVents && adjRoom.IsVent)
                continue;

            // Ignore adjacent rooms to force enemy to attack through vents
            if (ignoreAdjRooms && !adjRoom.IsVent && (adjRoom.CompareTag(NextToAdjacentRoomTag) || adjRoom.CompareTag(AdjacentRoomTag)))
                continue;

            weightedRooms = CalculateWeights(weightedRooms, adjRoom, currentRoom, ignoreVents, ignoreAdjRooms, currentWeight + 1);
        }

        return weightedRooms;
    }
}