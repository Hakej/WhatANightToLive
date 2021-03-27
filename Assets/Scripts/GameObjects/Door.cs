using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsClosed = false;
    public Animator DoorAnimator;

    public void Open()
    {
        IsClosed = false;

        RefreshAnimator();
    }

    public void Close()
    {
        IsClosed = true;

        RefreshAnimator();
    }

    public void RefreshAnimator()
    {
        DoorAnimator.SetBool("IsClosed", IsClosed);
    }
}
