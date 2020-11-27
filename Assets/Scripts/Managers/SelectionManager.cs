using System.Collections.Generic;
using Components.SelectionComponents;
using UnityEngine;

namespace Managers
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private Material _highlightMaterial;

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

                // deselect previously selected object
                Deselect(_selectableComponent);

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

                // select newly selected object
                Select(_selectableComponent);
            }
        }

        private SelectableComponent _selectableComponent;

        private Dictionary<Renderer, Material> _rendererOriginalMaterials = new Dictionary<Renderer, Material>();

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

        private void Select(SelectableComponent selectableComponent)
        {
            if (selectableComponent == null)
            {
                return;
            }

            var renderer = selectableComponent.GetComponent<Renderer>();
            // selected object have a renderer - highlight it
            if (renderer != null)
            {
                // memorize original material for selected object
                _rendererOriginalMaterials[renderer] = renderer.material;

                renderer.material = _highlightMaterial;
            }
        }

        private void Deselect(SelectableComponent selectableComponent)
        {
            if (selectableComponent == null)
            {
                return;
            }

            var renderer = selectableComponent.GetComponent<Renderer>();
            // previously selected object have a renderer - unhighlight it
            if (renderer != null)
            {
                if (!_rendererOriginalMaterials.ContainsKey(renderer))
                {
                    return;
                }

                // restore original material on previously selected object
                var originalMaterial = _rendererOriginalMaterials[renderer];
                _rendererOriginalMaterials.Remove(renderer);
                renderer.material = originalMaterial;
            }
        }
    }
}
