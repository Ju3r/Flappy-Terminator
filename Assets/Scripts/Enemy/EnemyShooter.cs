using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private float _delay = 5f;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private LayerMask _targetLayer;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _animator.AttackHitted += SpawnBullet;
    }

    private void OnDisable()
    {
        _animator.AttackHitted -= SpawnBullet;
    }

    public void StartAttacking()
    {
        if (gameObject.activeInHierarchy)
        {
            _coroutine = StartCoroutine(Attack());
        }
    }

    public void StopAttacking()
    {
        StopCoroutine(_coroutine);
    }

    public void SetBulletSpawner(BulletSpawner bulletSpawner)
    {
        _bulletSpawner = bulletSpawner;
    }

    private IEnumerator Attack()
    {
        while (enabled)
        {
            WaitForSeconds delay = new WaitForSeconds(_delay);

            yield return delay;

            StartAnimation();
        }
    }

    private void StartAnimation()
    {
        _animator.PlayAttackAnimation();
    }
    
    private void SpawnBullet()
    {
        _bulletSpawner.Spawn(transform.position, transform.rotation, _targetLayer);
    }
}