using Handlers;
using TMPro;
using UnityEngine;

namespace GameObjects
{
    public class Clock : MonoBehaviour
    {
        public TextMeshPro TimeText;
        public GameTimeHandler GameTimeHandler;

        private void Update()
        {
            TimeText.text = GameTimeHandler.CurrentGameTime.ToString();
        }
    }
}
