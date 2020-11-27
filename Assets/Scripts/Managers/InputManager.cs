using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public Action<KeyCode> KeyPressed = delegate {  };

        [SerializeField]
        private List<KeyCode> _trakedKeyCodes = new List<KeyCode>();

        private void Update()
        {
            if (_trakedKeyCodes.Any(Input.GetKeyDown))
            {
                var pressedKeys = _trakedKeyCodes.Where(Input.GetKeyDown);

                foreach (var pressedKey in pressedKeys)
                {
                    Debug.Log($"Key({pressedKey}) was pressed");

                    KeyPressed.Invoke(pressedKey);
                }
            }
        }
    }
}
