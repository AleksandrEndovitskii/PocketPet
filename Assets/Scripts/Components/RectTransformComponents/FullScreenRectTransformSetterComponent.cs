using UnityEngine;

namespace Components.RectTransformComponents
{
    public class FullScreenRectTransformSetterComponent : MonoBehaviour
    {
        private void Start()
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();

            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }
}