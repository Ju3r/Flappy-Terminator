using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        Vector3 position = transform.position;
        position.x = _target.transform.position.x + _xOffset;

        transform.position = position;
    }
}