using Controllers;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public ComputerController ComputerController;

    private void OnMouseDown()
    {
        ComputerController.ToggleFocus(true);
    }
}
