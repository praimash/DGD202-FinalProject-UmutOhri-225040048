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

   
}

