using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))] // ? Rigidbody zorunlu k�l�nd�
public class CubeMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] string speedParam = "Speed";

    private PlayerInput input;
    private InputAction moveAction;
    private Vector3 movement;
    private Rigidbody _rigidbody; // ? Rigidbody referans�

    public Vector3 CurrentMovement => movement;
    public float MoveSpeed => moveSpeed;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions["Move"];

        if (!animator) animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>(); // ? Rigidbody al�n�r
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // iste�e g�re de�i�tirilebilir
    }

    private void FixedUpdate() // ? Fiziksel hareketler i�in FixedUpdate
    {
        Vector2 inputVec = moveAction.ReadValue<Vector2>();
        movement = new Vector3(inputVec.x, 0, inputVec.y);

        if (movement.sqrMagnitude > 0.001f)
        {
            movement.Normalize();

            // ?? Hareket ve Yuvarlanma
            _rigidbody.linearVelocity = movement * moveSpeed;

            Vector3 rotationAxis = Vector3.Cross(Vector3.up, movement);
            _rigidbody.angularVelocity = rotationAxis * moveSpeed;

            // D�n�� animasyonu i�in
            Quaternion targetRot = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
        else
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        // ??? Animasyon kontrol�
        animator.SetFloat(speedParam, movement.magnitude);
    }
}

