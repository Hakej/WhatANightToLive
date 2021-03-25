using System.Collections;
using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        public static IEnumerator SetActiveAfterDelay(this GameObject obj, bool isActive, float delay)
        {
            yield return new WaitForSeconds(delay);

            obj?.SetActive(isActive);
        }
    }
}