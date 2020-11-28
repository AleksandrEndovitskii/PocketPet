using Managers;
using UnityEngine;

namespace Components.SelectionComponents
{
    public class IsSelectionTargetNotNullActivationComponent : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.Instance.SelectionManager.SelectedObjectChanged += OnSelectedObjectChanged;
        }
        private void Start()
        {
            OnSelectedObjectChanged(GameManager.Instance.SelectionManager.SelectedObject);
        }
        private void OnDestroy()
        {
            GameManager.Instance.SelectionManager.SelectedObjectChanged -= OnSelectedObjectChanged;
        }

        private void OnSelectedObjectChanged(SelectableComponent selectableComponent)
        {
            this.gameObject.SetActive(selectableComponent != null);
        }
    }
}
