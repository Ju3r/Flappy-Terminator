using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _prefab;
    [SerializeField] private int _defaultPoolSize = 20;
    [SerializeField] private int _maxPoolSize = 50;
    [SerializeField] private Transform _spawnPoint;

    private ObjectPool<Bullet> _pool;
    private List<Bullet> _bullets;

    private void Awake()
    {
        _bullets = new List<Bullet>();

        _pool = new ObjectPool<Bullet>(
            createFunc: () => Create(),
            actionOnGet: (bullet) => ModifyOnGet(bullet),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet),
            collectionCheck: true,
            defaultCapacity: _defaultPoolSize,
            maxSize: _maxPoolSize);
    }

    private void OnDisable()
    {
        foreach (Bullet bullet in _bullets)
        {
            Unsubscription(bullet);
        }

        _bullets.Clear();
    }

    public void Reset()
    {
        foreach (Bullet enemy in _bullets)
        {
            if (enemy.gameObject.activeInHierarchy)
                ReleaseInPool(enemy);
        }
    }

    public void Spawn(Vector3 positionToSpawn, Quaternion rotationToSpawn, LayerMask targetLayer)
    {
        Bullet bullet = _pool.Get();
        bullet.Init(positionToSpawn, rotationToSpawn, targetLayer);
    }

    private Bullet Create()
    {
        Bullet bullet = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
        Subscription(bullet);
        _bullets.Add(bullet);

        return bullet;
    }

    private void ModifyOnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void ReleaseInPool(Bullet bullet)
    {
        _pool.Release(bullet);
        bullet.gameObject.SetActive(false);
    }

    private void Subscription(Bullet bullet)
    {
        bullet.Destroyed += ReleaseInPool;
    }

    private void Unsubscription(Bullet bullet)
    {
        bullet.Destroyed -= ReleaseInPool;
    }
}