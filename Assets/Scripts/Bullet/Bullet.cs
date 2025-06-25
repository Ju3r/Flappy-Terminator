using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(BulletCollisionHandler))]
public class Bullet : MonoBehaviour, IInteractable
{
    private Mover _mover;
    private BulletCollisionHandler _collisionHandler;
    private bool _isDestroyed;

    public event Action<Bullet> Destroyed;

    [field: SerializeField] public LayerMask TargetLayer { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _collisionHandler = GetComponent<BulletCollisionHandler>();
        _isDestroyed = false;
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += Destroy;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= Destroy;
    }

    private void Update()
    {
        _mover.Move(transform.right);
    }

    public void Init(Vector2 positionToSpawn, Quaternion rotation, LayerMask targetLayer)
    {
        transform.position = positionToSpawn;
        transform.rotation = rotation;
        TargetLayer = targetLayer;
        _isDestroyed = false;
    }

    private void Destroy()
    {
        if (_isDestroyed)
            return;

        _isDestroyed = true;
        Destroyed?.Invoke(this);    
    }
}