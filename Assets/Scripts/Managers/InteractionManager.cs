using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField]
        private List<KeyCode> _interactableKeyCodes = new List<KeyCode>();

        public void Initialize()
        {
            GameManager.Instance.InputManager.KeyPressed += OnKeyPressed;
        }

        private void OnKeyPressed(KeyCode keyCode)
        {
            if (!_interactableKeyCodes.Contains(keyCode))
            {
                return;
            }
        }
    }
}
