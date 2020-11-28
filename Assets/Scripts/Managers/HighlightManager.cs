using System;
using System.Collections.Generic;
using Components.SelectionComponents;
using UnityEngine;
using Utils;

namespace Managers
{
    public class HighlightManager : MonoBehaviour, IInitilizable
    {
        public Action<SelectableComponent> HighlightedObjectChanged = delegate {  };

        [SerializeField]
        private Material _highlightMaterial;

        public SelectableComponent HighlightedObject
        {
            get
            {
                return _highlightedObject;
            }
            set
            {
                if (_highlightedObject == value)
                {
                    return;
                }

                // deselect previously selected object
                UnHighlight(_highlightedObject);

                var previousObjectName = "None";
                if (_highlightedObject != null &&
                    _highlightedObject.gameObject != null)
                {
                    previousObjectName = _highlightedObject.gameObject.name;
                }

                var currentObjectName = "None";
                if (value != null &&
                    value.gameObject != null)
                {
                    currentObjectName = value.gameObject.name;
                }

                Debug.Log($"Highlighted gameobject changed from {previousObjectName} to {currentObjectName}");

                _highlightedObject = value;

                // select newly selected object
                Highlight(_highlightedObject);

                HighlightedObjectChanged.Invoke(_highlightedObject);
            }
        }

        private SelectableComponent _highlightedObject;

        private Dictionary<Renderer, Material> _rendererOriginalMaterials = new Dictionary<Renderer, Material>();

        public void Initialize()
        {
            GameManager.Instance.SelectionManager.SelectedObjectChanged += OnSelectableComponentChanged;
        }

        private void OnSelectableComponentChanged(SelectableComponent selectableComponent)
        {
            HighlightedObject = selectableComponent;
        }

        private void Highlight(SelectableComponent selectableComponent)
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

        private void UnHighlight(SelectableComponent selectableComponent)
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
