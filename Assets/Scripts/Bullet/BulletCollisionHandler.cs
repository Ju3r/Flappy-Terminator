using System;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class BulletCollisionHandler : MonoBehaviour
{
    private Bullet _bullet;

    public event Action CollisionDetected;

    private void Awake()
    {
        _bullet = GetComponent<Bullet>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            if (interactable is Bullet)
                return;
           
            CollisionDetected?.Invoke();
        }

        if (collision.TryGetComponent(out IDamageable damageable))
        {
            if ((1 << collision.gameObject.layer & _bullet.TargetLayer.value) != 0)
                CollisionDetected?.Invoke();
        }
    }
}
