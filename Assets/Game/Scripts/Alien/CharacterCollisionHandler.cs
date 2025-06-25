using System;
using UnityEngine;

public class CharacterCollisionHandler : MonoBehaviour
{
    public event Action CollisionDetected;
    public event Action ShotDowned;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            if (interactable is Bullet bullet)
            {
                if ((1 << gameObject.layer & bullet.TargetLayer.value) != 0)
                    ShotDowned?.Invoke();
            }
            else
            {
                CollisionDetected?.Invoke();
            }
        }
    }
}
