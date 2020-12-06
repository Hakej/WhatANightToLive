using TMPro;
using UnityEngine;

namespace Handlers
{
    public class DebugHandler : Singleton<DebugHandler>
    {
        public bool IsDebugModeOn = true;
        public Canvas DebugUICanvas;
        public TextMeshProUGUI DebugInfo;

        private void Start()
        {
            UpdateDebug();
        }

        public void Update()
        {
            if (!IsDebugModeOn)
            {
                return;
            }

            var gh = GameHandler.Instance;
        
            DebugInfo.text = $"Current sanity level: {gh.Sanity.CurrentSanity}\n" +
                             $"Current fear level: {gh.Sanity.CurrentFearLevel}\n" +
                             $"Current sanity drop: {gh.Sanity.CurrentSanityDrop}";
        }

        public void ToggleDebugMode()
        {
            IsDebugModeOn = !IsDebugModeOn;
            UpdateDebug();
        }

        private void UpdateDebug()
        {
            DebugUICanvas.gameObject.SetActive(IsDebugModeOn);
        }
    }
}