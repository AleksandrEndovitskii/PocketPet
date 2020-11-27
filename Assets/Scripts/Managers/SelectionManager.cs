using System;
using Components.SelectionComponents;
using UnityEngine;

namespace Managers
{
    public class SelectionManager : MonoBehaviour
    {
        public Action<SelectableComponent> SelectableComponentChanged = delegate {  };

        public SelectableComponent SelectableComponent
        {
            get
            {
                return _selectableComponent;
            }
            set
            {
                if (_selectableComponent == value)
                {
                    return;
                }

                var previouslySelectedObjectName = "None";
                if (_selectableComponent != null &&
                    _selectableComponent.gameObject != null)
                {
                    previouslySelectedObjectName = _selectableComponent.gameObject.name;
                }

                var currentlySelectedObjectName = "None";
                if (value != null &&
                    value.gameObject != null)
                {
                    currentlySelectedObjectName = value.gameObject.name;
                }

                Debug.Log(
                    $"Selected gameobject changed from {previouslySelectedObjectName} to {currentlySelectedObjectName}");

                _selectableComponent = value;

                SelectableComponentChanged.Invoke(_selectableComponent);
            }
        }

        private SelectableComponent _selectableComponent;

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // dont hit anything - nothing to do here
            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            SelectableComponent = hit.transform.gameObject.GetComponent<SelectableComponent>();
        }
    }
}
