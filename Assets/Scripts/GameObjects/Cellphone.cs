using Controllers;
using UnityEngine;

public class Cellphone : MonoBehaviour
{
    private void OnMouseDown()
    {
        PlayerCellphoneController.Instance.PhoneToggle();
    }
}
