using Classes.Abstracts;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenericEnemy : Enemy
{
    protected override void Move()
    {
        var curRoom = CurrentRoom.GetComponent<Room>();
        var adjRooms = curRoom.AdjacentRooms;
        var newRoom = adjRooms[Random.Range(0, adjRooms.Length)];

        ChangeRoom(newRoom);
        
        Debug.Log($"{gameObject.name}: I've successfully moved to {newRoom.name}.");
        
        if (newRoom.CompareTag("PlayerAdjacentRoom"))
        {
            ChangeToAttackMode();
        }
    }
}
