using Handlers;
using UnityEngine;

public class DoorsHandler : MonoBehaviour
{
    public Door CurrentClosedDoor;

    public void ChangeClosedDoor(Door newClosedDoor)
    {
        var oldClosedDoor = CurrentClosedDoor;

        CurrentClosedDoor?.Open();

        if (CurrentClosedDoor == newClosedDoor)
        {
            CurrentClosedDoor = null;
        }
        else
        {
            CurrentClosedDoor = newClosedDoor;
            CurrentClosedDoor.Close();
        }

        EventHandler.Instance.ChangedClosedDoor(oldClosedDoor, CurrentClosedDoor);
    }
}
