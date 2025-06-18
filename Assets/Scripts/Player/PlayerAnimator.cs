using UnityEngine;
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private CubeMover _cubeMover;  // Inspector'dan ata
    [SerializeField] private Transform _mesh;

    private float _moveSpeed;

    private void Awake()
    {
        if (_cubeMover == null)
            _cubeMover = GetComponent<CubeMover>();
    }

    private void Start()
    {
        _moveSpeed = _cubeMover.MoveSpeed;
    }

    private void Update()
    {
        Vector3 velocity = _cubeMover.CurrentMovement;
        float forwardVelocity = Vector3.Dot(velocity, transform.forward);
        if (forwardVelocity != 0)
        {
            _mesh.localRotation *= Quaternion.Euler(Mathf.Deg2Rad * (360 / forwardVelocity), 0, 0);
        }
    }
}



