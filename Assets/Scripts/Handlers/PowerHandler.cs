using UnityEngine;
using Singletons;

namespace Handlers
{
    public class PowerHandler : MonoBehaviour
    {
        public bool IsPowerOn;

        public void TogglePower()
        {
            IsPowerOn = !IsPowerOn;

            EventManager.Instance.PowerToggle(IsPowerOn);
        }
    }
}