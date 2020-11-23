using UnityEngine;
using UnityEngine.UI;

namespace Components.RectTransformComponents
{
    [RequireComponent(typeof(Button))]
    public class DestroyRectTransformButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;

        private Button _button;

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();

            _button.onClick.AddListener(ButtonOnClick);
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonOnClick);
        }

        private void ButtonOnClick()
        {
            Debug.Log("DestroyRectTransformButtonComponent button is pressed");

            Destroy(_rectTransform.gameObject);
        }
    }
}
