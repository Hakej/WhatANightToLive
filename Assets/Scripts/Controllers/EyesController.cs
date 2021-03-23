using System.Collections;
using UnityEngine;
using Handlers;
using UnityEngine.UI;

namespace Controllers
{
    public class EyesController : Singleton<EyesController>
    {
        public Animator EyesAnimator;
        public AudioLowPassFilter AudioLowPassFilter;

        public float MinFrequency;
        public float MaxFrequency;
        public float FilterChangeTime;

        [Header("Eyes Button")]
        public Image Image;
        public Sprite OpenedEye;
        public Sprite ClosedEye;

        public void ToggleEyes(bool areEyesClosed)
        {
            EventHandler.Instance.EyesToggle(areEyesClosed);

            EyesAnimator.SetBool("AreEyesClosed", areEyesClosed);

            StartCoroutine(ToggleAudioLowPassFilter(areEyesClosed));

            Image.sprite = areEyesClosed ? ClosedEye : OpenedEye;
        }

        public IEnumerator ToggleAudioLowPassFilter(bool isLowering)
        {
            var currentTime = 0f;

            if (isLowering)
            {
                while (currentTime < FilterChangeTime)
                {
                    currentTime += Time.deltaTime;
                    AudioLowPassFilter.cutoffFrequency = Mathf.Lerp(MaxFrequency, MinFrequency, currentTime / FilterChangeTime);
                    yield return null;
                }

                AudioLowPassFilter.cutoffFrequency = MinFrequency;
            }
            else
            {
                while (currentTime < FilterChangeTime)
                {
                    currentTime += Time.deltaTime;
                    AudioLowPassFilter.cutoffFrequency = Mathf.Lerp(MinFrequency, MaxFrequency, currentTime / FilterChangeTime);
                    yield return null;
                }

                AudioLowPassFilter.cutoffFrequency = MaxFrequency;
            }
        }
    }
}