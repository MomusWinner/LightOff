using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    [SerializeField] float _speed;
    [SerializeField] float _rotationSpeed;
    [Space(5)]
    [Header("Input")]
    [SerializeField] InputAction _moveAction;
    [SerializeField] InputAction _lookAction;

    private CharacterController _characterController;
    private Vector2 cameraRotation;
    
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move(_moveAction.ReadValue<Vector2>());
        Look(_lookAction.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
            return;
        var scaledMoveSpeed = _speed * Time.deltaTime;
        var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        _characterController.Move(move * scaledMoveSpeed);
    }
    
    private void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.01)
            return;
        var scaledRotateSpeed = _rotationSpeed * Time.deltaTime;
        cameraRotation.y += rotate.x * scaledRotateSpeed;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x - rotate.y * scaledRotateSpeed, -89, 89);
        _camera.transform.localEulerAngles = new Vector2(cameraRotation.x, 0);
        transform.localEulerAngles = new Vector2(0, cameraRotation.y);
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _lookAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
    }
}
