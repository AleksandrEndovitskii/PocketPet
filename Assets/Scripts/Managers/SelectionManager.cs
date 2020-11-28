using System;
using Components.SelectionComponents;
using UnityEngine;

namespace Managers
{
    public class SelectionManager : MonoBehaviour
    {
        public Action<SelectableComponent> SelectedObjectChanged = delegate {  };

        public SelectableComponent SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            set
            {
                if (_selectedObject == value)
                {
                    return;
                }

                var previouslySelectedObjectName = "None";
                if (_selectedObject != null &&
                    _selectedObject.gameObject != null)
                {
                    previouslySelectedObjectName = _selectedObject.gameObject.name;
                }

                var currentlySelectedObjectName = "None";
                if (value != null &&
                    value.gameObject != null)
                {
                    currentlySelectedObjectName = value.gameObject.name;
                }

                Debug.Log(
                    $"Selected gameobject changed from {previouslySelectedObjectName} to {currentlySelectedObjectName}");

                _selectedObject = value;

                SelectedObjectChanged.Invoke(_selectedObject);
            }
        }

        private SelectableComponent _selectedObject;

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // dont hit anything - nothing to do here
            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            SelectedObject = hit.transform.gameObject.GetComponent<SelectableComponent>();
        }
    }
}
