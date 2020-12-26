using Controllers;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    private PlayerFocusController _focus;
    
    private void Start()
    {
        var player = GameObject.FindWithTag("Player");
        _focus = player.GetComponent<PlayerFocusController>();
    }

    private void OnMouseDown()
    {
        if (PlayerCellphoneController.Instance.IsPlayerOnPhone) return;
        
       _focus.ToggleFocus();
    }
}
