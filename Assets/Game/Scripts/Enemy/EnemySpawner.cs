using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _prefabs;
    [SerializeField] private List<SpawnPosition> _spawnPositions;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private int _defaultPoolSize = 20;
    [SerializeField] private int _maxPoolSize = 50;
    [SerializeField] private float _spawnRate = 5;

    private List<Enemy> _enemies;
    private float _rotationY = 180;
    private float _outOfAngle = 0;
    private ObjectPool<Enemy> _pool;
    private System.Random _random;
    private bool _isActive = true;

    public event Action<float> EnemyKilledByPlayer;

    private void Awake()
    {
        _enemies = new List<Enemy>();
        _random = new System.Random();

        _pool = new ObjectPool<Enemy>(
            createFunc: () => Create(),
            actionOnGet: (enemy) => ModifyOnGet(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _defaultPoolSize,
            maxSize: _maxPoolSize);
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
        {
            Unsubscription(enemy);
        }

        _enemies.Clear();
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deativate()
    {
        _isActive = false;
    }

    public void Reset()
    {
        foreach (Enemy enemy in _enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
                ReleaseInPool(enemy);
        }
    }

    private Enemy Create()
    {
        Enemy enemy = Instantiate(ChooseRandomPrefab(), Vector3.zero, Quaternion.Euler(_outOfAngle, _rotationY, _outOfAngle));
        enemy.Init(_bulletSpawner);

        Subscription(enemy);
        _enemies.Add(enemy);

        return enemy;
    }

    private void ModifyOnGet(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.StartAttacking();
    }

    private void ReleaseInPool(Enemy enemy)
    {
        enemy.StopAttacking();
        _pool.Release(enemy);
    }

    private void DieByPlayer(Enemy enemy)
    {
        enemy.StopAttacking();
        _pool.Release(enemy);
        EnemyKilledByPlayer?.Invoke(enemy.PointToKill);
    }

    private Enemy ChooseRandomPrefab()
    {
        int startIndex = 0;
        int prefabIndex = _random.Next(startIndex, _prefabs.Count);

        return _prefabs[prefabIndex];
    }

    private void SetOnSpawnPosition(Enemy enemy, SpawnPosition spawnPosition)
    {
        enemy.transform.position = spawnPosition.transform.position;
    }

    private IEnumerator Spawning()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnRate);

        while (_isActive)
        {
            for (int i = 0; i < _spawnPositions.Count; i++)
            {
                Enemy enemy = _pool.Get();
                SetOnSpawnPosition(enemy, _spawnPositions[i]);
            }
            yield return wait;
        }
    }

    private void Subscription(Enemy enemy)
    {
        enemy.Died += ReleaseInPool;
        enemy.DiedByPlayer += DieByPlayer;
    }

    private void Unsubscription(Enemy enemy)
    {
        enemy.Died -= ReleaseInPool;
        enemy.DiedByPlayer -= DieByPlayer;
    }
}