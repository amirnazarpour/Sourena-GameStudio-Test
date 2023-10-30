using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPerson : MonoBehaviour
    {
        private CharacterController _characterController;
        private DefualtInput _defualtInput;
        public Vector2 input_Movement;
        public Vector2 input_View;


        private Vector3 newCameraRotation;
        private Vector3 newCharacterRotation;

        [Header("Reference")] [SerializeField] private Transform cameraHolder;

        [Header("Setting")] [SerializeField] private PlayerSettingModle playerSettings;

        [SerializeField] private Vector2 viewClampY = new Vector2(-70, 80);


        [Header("Gravity")] public float gravityAmount;
        public float gravityMin;
        private float _playerGravity;

        public Vector3 jumpingForce;
        public Vector3 jumpingForceVelocity;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _defualtInput = new DefualtInput();

            _defualtInput.Character.Movement.performed += value => input_Movement = value.ReadValue<Vector2>();
            _defualtInput.Character.View.performed += value => input_View = value.ReadValue<Vector2>();


            _defualtInput.Character.Jump.performed += value => Jump();

            _defualtInput.Enable();

            newCameraRotation = cameraHolder.localRotation.eulerAngles;
            newCharacterRotation = transform.localRotation.eulerAngles;
        }

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            CalculateView();
            CalculateMovement();
            CalculateJump();
        }


        private void CalculateView()
        {
            float inputX = playerSettings.viewXInverted ? -input_View.x : input_View.x;

            newCharacterRotation.y += playerSettings.viewSensitivity.x * inputX * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(newCharacterRotation);

            float inputY = playerSettings.viewYInverted ? input_View.y : -input_View.y;

            newCameraRotation.x += playerSettings.viewSensitivity.y * inputY * Time.deltaTime;
            newCameraRotation = Vector3.right * Mathf.Clamp(newCameraRotation.x, viewClampY.x, viewClampY.y);

            cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
        }

        private void CalculateMovement()
        {
            var verticalSpeed = playerSettings.walkingForwardSpeed * input_Movement.y * Time.deltaTime;
            var horizontalSpeed = playerSettings.walkingStrafeSpeed * input_Movement.x * Time.deltaTime;

            var newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);
            newMovementSpeed = transform.TransformDirection(newMovementSpeed);


            if (_playerGravity > gravityMin && jumpingForce.y < 0.1f)
            {
                _playerGravity -= gravityAmount * Time.deltaTime;
            }

            if (_playerGravity < -1 && _characterController.isGrounded)
            {
                _playerGravity = -1;
            }

            if (jumpingForce.y > 0.1f)
            {
                _playerGravity = 0;
            }

            newMovementSpeed.y += _playerGravity;

            newMovementSpeed += jumpingForce * Time.deltaTime;

            _characterController.Move(newMovementSpeed);
        }

        public void CalculateJump()
        {
            jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity,
                playerSettings.jumpingFalloff);
        }

        private void Jump()
        {
            if (!_characterController.isGrounded)
            {
                return;
            }

            // jump
            jumpingForce = Vector3.up * playerSettings.jumpingHeight;
        }
    }
}