using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(EnemyShooter), typeof(CharacterCollisionHandler))]
public class Enemy : MonoBehaviour, IDamageable
{
    private Mover _mover;
    private EnemyShooter _shooter;
    private CharacterCollisionHandler _collisionHandler;

    public event Action<Enemy> Died;
    public event Action<Enemy> DiedByPlayer;

    [field: SerializeField] public float PointToKill { get; private set; } = 1;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _shooter = GetComponent<EnemyShooter>();
        _collisionHandler = GetComponent<CharacterCollisionHandler>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += Die;
        _collisionHandler.ShotDowned += DieByPlayer;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= Die;
        _collisionHandler.ShotDowned -= DieByPlayer;
    }

    private void Update()
    {
        _mover.Move(transform.right);
    }

    public void Init(BulletSpawner bulletSpawner)
    {
        _shooter.SetBulletSpawner(bulletSpawner);
    }

    public void StartAttacking()
    {
        _shooter.StartAttacking();
    }

    public void StopAttacking()
    {
        _shooter.StopAttacking();
    }

    private void Die()
    {
        Died?.Invoke(this);
    }

    private void DieByPlayer()
    {
        DiedByPlayer?.Invoke(this);
    }
}
