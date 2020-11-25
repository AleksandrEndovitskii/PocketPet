using UnityEngine;

namespace Components.CharacterComponents
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerMovementComponent : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 10f;

        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = this.gameObject.GetComponent<CharacterController>();
        }

        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            var move = this.gameObject.transform.right * x + this.gameObject.transform.forward * z;

            _characterController.Move(move * _speed * Time.deltaTime);
        }
    }
}
