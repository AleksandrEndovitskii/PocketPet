using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private string _selectableTag = "Selectable";
        [SerializeField] private Material _highlightMaterial;

        private Transform _selectedTransform;

        private Dictionary<Renderer, Material> _rendererOriginalMaterials = new Dictionary<Renderer, Material>();

        private void Update()
        {
            Renderer selectedTransformRenderer;

            if (_selectedTransform != null)
            {
                selectedTransformRenderer = _selectedTransform.GetComponent<Renderer>();

                var originalMaterial = _rendererOriginalMaterials[selectedTransformRenderer];
                _rendererOriginalMaterials.Remove(selectedTransformRenderer);
                selectedTransformRenderer.material = originalMaterial;

                _selectedTransform = null;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // dont hit anything - nothing to do here
            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            var selectedTransform = hit.transform;
            // hit something without selectable tag - nothing to do here
            if (!selectedTransform.CompareTag(_selectableTag))
            {
                return;
            }

            selectedTransformRenderer = selectedTransform.GetComponent<Renderer>();
            // hit something with renderer - highlight it
            if (selectedTransformRenderer != null)
            {
                _rendererOriginalMaterials[selectedTransformRenderer] = selectedTransformRenderer.material;

                selectedTransformRenderer.material = _highlightMaterial;
            }

            _selectedTransform = selectedTransform;
        }
    }
}
