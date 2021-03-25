using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utility
{
    public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _pointerDown;
        private float _pointerDownTimer;

        [SerializeField]
        private float _requiredHoldTime;

        public UnityEvent OnLongClick;

        [SerializeField]
        private Image _fillImage;

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Reset();
        }

        private void Update()
        {
            if (!_pointerDown)
            {
                return;
            }

            _pointerDownTimer += Time.deltaTime;

            if (_pointerDownTimer >= _requiredHoldTime)
            {
                OnLongClick?.Invoke();
                Reset();
            }

            _fillImage.fillAmount = _pointerDownTimer / _requiredHoldTime;
        }

        private void Reset()
        {
            _pointerDown = false;
            _pointerDownTimer = 0f;
            _fillImage.fillAmount = 0f;
        }
    }
}