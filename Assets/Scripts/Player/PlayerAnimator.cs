using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private CubeMover _cubeMover;       // ✅ CubeMover ile eşleşti
    [SerializeField] private Transform _mesh;

    private float _moveSpeed;

    private void Awake()
    {
        _cubeMover = GetComponent<CubeMover>();
    }

    private void Start()
    {
        _moveSpeed = _cubeMover.MoveSpeed;
    }

    private void Update()
    {
        Vector3 velocity = _cubeMover.CurrentMovement; // ✅ Rigidbody yerine CubeMover'dan okuduk
        float forwardVelocity = Vector3.Dot(velocity, transform.forward);
        if (forwardVelocity != 0)
        {
            _mesh.localRotation *= Quaternion.Euler(Mathf.Deg2Rad * (360 / forwardVelocity), 0, 0);
        }
    }
}

