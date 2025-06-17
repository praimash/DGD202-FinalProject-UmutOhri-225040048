using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private InputAction _moveAction;

    [Header("Movement Settings")]
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
    [SerializeField] private float _turnSpeed = 180f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Move");
        _moveAction.Enable();
    }

    private void Update()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        HandleMovement(input);
    }

    private void HandleMovement(Vector2 direction)
    {
        // Rotation
        float rotationAmount = direction.x * _turnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);

        // Movement
        Vector3 moveDirection = transform.forward * (direction.y * MoveSpeed);
        _rigidbody.linearVelocity = new Vector3(moveDirection.x, _rigidbody.linearVelocity.y, moveDirection.z);
    }
}