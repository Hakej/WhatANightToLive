using UnityEngine;

namespace Controllers
{
    public class PlayerCellphoneController : Singleton<PlayerCellphoneController>
    {
        public GameObject CellphoneObject;
        public GameObject CellphoneUI;
    
        public bool IsPlayerOnPhone = false;
        
        public void PhoneToggle()
        {
            IsPlayerOnPhone = !IsPlayerOnPhone;
            
            CellphoneObject.SetActive(!IsPlayerOnPhone);
            CellphoneUI.SetActive(IsPlayerOnPhone);
        }
    }
}
