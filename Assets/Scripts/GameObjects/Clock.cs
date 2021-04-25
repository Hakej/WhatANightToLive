using Handlers;
using TMPro;
using UnityEngine;

namespace GameObjects
{
    public class Clock : MonoBehaviour
    {
        public TextMeshPro TimeText;

        private void Update()
        {
            TimeText.text = GameTimeHandler.Instance.CurrentGameTime.ToString();
        }
    }
}
