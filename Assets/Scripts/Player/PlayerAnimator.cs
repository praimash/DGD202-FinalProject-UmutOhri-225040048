using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private CubeMover _cubeMover;
    [SerializeField] private Transform _mesh;

    private void Awake()
    {
        if (_cubeMover == null)
            _cubeMover = GetComponent<CubeMover>();
    }

    private void Update()
    {
        if (_cubeMover == null || _mesh == null)
        {
            
            return;
        }

        Vector3 velocity = _cubeMover.CurrentMovement;
        if (velocity.sqrMagnitude > 0.01f)
        {
            float rotateAmount = 360f * Time.deltaTime * 50f; // D�nme h�z� (iste�e g�re ayarla)
            _mesh.Rotate(Vector3.right, rotateAmount);
        }
    }
}
