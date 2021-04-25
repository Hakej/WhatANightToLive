using System.Collections.Generic;
using System.Text;
using Classes.Abstracts;
using TMPro;
using UnityEngine;

namespace Handlers
{
    public class DebugController : Singleton<DebugController>
    {
        public KeyCode ToggleDebugKey;

        private void Update()
        {
            if (Input.GetKeyDown(ToggleDebugKey))
            {
                DebugHandler.Instance.ToggleDebugMode();
            }
        }
    }
}