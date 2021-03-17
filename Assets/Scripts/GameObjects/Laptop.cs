using Controllers;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    public LaptopController LaptopController;

    private void OnMouseDown()
    {
        LaptopController.ToggleFocus(true);
    }
}
