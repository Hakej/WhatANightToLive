using Handlers;
using UnityEngine;
using UnityEngine.UI;

public class DoorButton : MonoBehaviour
{
    public Door ReferencedDoor;

    [Header("UI")]
    public Image Image;
    public Sprite DoorOpen;
    public Sprite DoorClosed;

    private void OnEnable()
    {
        CheckSprite();

        EventHandler.Instance.OnChangedClosedDoor += OnChangedClosedDoor;
    }

    private void OnChangedClosedDoor(Door oldClosedDoor, Door newClosedDoor)
    {
        if (ReferencedDoor == oldClosedDoor || ReferencedDoor == newClosedDoor)
        {
            CheckSprite();
        }
    }

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

        CheckSprite();
    }

    public void CheckSprite()
    {
        Image.sprite = ReferencedDoor.IsClosed ? DoorClosed : DoorOpen;

        Image.color = ReferencedDoor.IsClosed ? Color.red : Color.blue;
    }
}
