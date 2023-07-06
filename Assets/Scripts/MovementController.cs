using UnityEngine;

namespace Assets.Scripts
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed, rotationSpeed;
        private Transform _transform;
        private float _rotationX, _mouseX;
        private bool _mouseClick;

        private void Start()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            _transform.Translate(Input.GetAxis("Horizontal") * movementSpeed * -1, 0,
                Input.GetAxis("Vertical") * movementSpeed * -1);
            if (!_mouseClick) return;
            _rotationX += (_mouseX - Input.GetAxis("Mouse X")) * rotationSpeed * -1;
            _rotationX = Mathf.Clamp(_rotationX, -180, 180);
            _transform.rotation = Quaternion.Euler(Vector3.up * _rotationX);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseClick = true;
                _mouseX = Input.GetAxis("Mouse X");
            }
            
            else if (Input.GetMouseButtonUp(0))
            {
                _mouseClick = false;
                _mouseX = 0;
            }
            
        }
    }
}
