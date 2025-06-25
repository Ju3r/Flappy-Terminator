using UnityEngine;

public class AlienShooter : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private LayerMask _targetLayer;

    public void Shoot()
    {
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        _bulletSpawner.Spawn(_bulletSpawnPoint.position, transform.rotation, _targetLayer);
    }
}