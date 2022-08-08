using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _smoothedSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothedSpeed);
        transform.position = smoothedPosition;
        //transform.LookAt(_target);
    }
}