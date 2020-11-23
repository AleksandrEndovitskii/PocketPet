using UnityEngine;
using UnityEngine.UI;

namespace Components.RectTransformComponents
{
    [RequireComponent(typeof(Button))]
    public class InstantiateRectTransformButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransformPrefab;

        private RectTransform _rectTransformInstance;

        private Button _button;
        private Canvas _canvas;

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
            _canvas = gameObject.GetComponentInParent<Canvas>();

            _button.onClick.AddListener(ButtonOnClick);
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonOnClick);
        }

        private void ButtonOnClick()
        {
            Debug.Log("InstantiateRectTransformButtonComponent button is pressed");

            if (_rectTransformInstance == null)
            {
                _rectTransformInstance = Instantiate(_rectTransformPrefab, _canvas.gameObject.transform);
            }
            else
            {
                Destroy(_rectTransformInstance.gameObject);
                _rectTransformInstance = null;
            }
        }
    }
}