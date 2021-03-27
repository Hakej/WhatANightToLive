using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public Door ReferencedDoor;

    public void ToggleDoor()
    {
        if (ReferencedDoor.IsClosed)
        {
            ReferencedDoor.Open();
        }
        else
        {
            ReferencedDoor.Close();
        }
    }
}
