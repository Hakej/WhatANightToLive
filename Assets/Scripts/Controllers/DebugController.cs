using System.Collections.Generic;
using System.Text;
using Classes.Abstracts;
using TMPro;
using UnityEngine;

namespace Handlers
{
    public class DebugController : MonoBehaviour
    {
        public KeyCode ToggleDebugKey;

        private DebugHandler _debugHandler;

        private void Start()
        {
            _debugHandler = FindObjectOfType<DebugHandler>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(ToggleDebugKey))
            {
                _debugHandler.ToggleDebugMode();
            }
        }
    }
}