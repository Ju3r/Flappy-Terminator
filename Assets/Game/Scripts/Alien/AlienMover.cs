using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class AlienMover : MonoBehaviour
{
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Rigidbody2D _rigidbody;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler (0.0f, 0.0f, _maxRotationZ);
        _minRotation = Quaternion.Euler (0.0f, 0.0f, _minRotationZ);
    }

    private void Update()
    {
        if (enabled)
            transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    public void Activate()
    {
        enabled = true;
    }

    public void Deactivate()
    {
        enabled = false;
    }

    public void Tap()
    {
        _rigidbody.velocity = new Vector2(_speed, _tapForce);
        transform.rotation = _maxRotation;
    }

    public void Reset()
    {
        _rigidbody.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }
}
