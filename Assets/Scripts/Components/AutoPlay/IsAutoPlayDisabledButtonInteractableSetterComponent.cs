using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Components.AutoPlay
{
    [RequireComponent(typeof(Button))]
    public class IsAutoPlayDisabledButtonInteractableSetterComponent : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = this.gameObject.GetComponent<Button>();

            GameManager.Instance.AutoPlayManager.IsAutoplayOnChanged += OnIsAutoplayOnChanged;
        }
        private void Start()
        {
            OnIsAutoplayOnChanged(GameManager.Instance.AutoPlayManager.IsAutoplayOn);
        }
        private void OnDestroy()
        {
            if (GameManager.Instance != null &&
                GameManager.Instance.AutoPlayManager != null)
            {
                GameManager.Instance.AutoPlayManager.IsAutoplayOnChanged -= OnIsAutoplayOnChanged;
            }
        }

        private void OnIsAutoplayOnChanged(bool isAutoplayOn)
        {
            _button.interactable = !isAutoplayOn;
        }
    }
}
