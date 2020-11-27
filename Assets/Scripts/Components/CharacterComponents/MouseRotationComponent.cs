using UnityEngine;

namespace Components.CharacterComponents
{
    public class MouseRotationComponent : MonoBehaviour
    {
        [SerializeField]
        private float _mouseSensetivity = 100f;

        public Transform _playerBody;

        private float _xRotation = 0f;

        private void Update()
        {
            var mouseX = Input.GetAxis("Mouse X") * _mouseSensetivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * _mouseSensetivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            this.gameObject.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
