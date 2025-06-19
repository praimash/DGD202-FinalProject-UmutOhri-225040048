using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
    [SerializeField] private float _turnSpeed = 180f;
    [SerializeField] private Animator _animator;

    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private InputAction _moveAction;

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

        // Animasyon parametresi g�ncelle
        _animator.SetFloat("Speed", Mathf.Abs(input.y));
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
