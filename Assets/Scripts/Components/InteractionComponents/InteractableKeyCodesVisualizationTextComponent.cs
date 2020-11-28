using Managers;
using TMPro;
using UnityEngine;

namespace Components.InteractionComponents
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class InteractableKeyCodesVisualizationTextComponent : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = this.gameObject.GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            _text.text = GameManager.Instance.InteractionManager.InteractableKeyCodes.ToString<KeyCode>();
        }
    }
}
