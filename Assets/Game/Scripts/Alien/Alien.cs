using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(AlienMover), typeof(AlienScore))]
[RequireComponent(typeof(CharacterCollisionHandler), typeof(AlienShooter))]
public class Alien : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform _spawnPoint;

    private InputReader _inputReader;
    private AlienMover _mover;
    private AlienScore _score;
    private CharacterCollisionHandler _colissionHandler;
    private AlienShooter _shooter;

    public event Action Died;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<AlienMover>();
        _score = GetComponent<AlienScore>();
        _colissionHandler = GetComponent<CharacterCollisionHandler>();
        _shooter = GetComponent<AlienShooter>();
    }

    private void OnEnable()
    {
        _inputReader.Tapped += Tap;
        _inputReader.ShootPressed += Shoot;
        _colissionHandler.CollisionDetected += Die;
        _colissionHandler.ShotDowned += Die;
    }

    private void OnDisable()
    {
        _inputReader.Tapped -= Tap;
        _inputReader.ShootPressed -= Shoot;
        _colissionHandler.CollisionDetected -= Die;
        _colissionHandler.ShotDowned -= Die;
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _score.Reset();
        _mover.Reset();
        transform.position = _spawnPoint.position;
    }

    public void ActivateMover()
    {
        _mover.Activate();
    }

    public void DeactivateMover()
    {
        _mover.Deactivate();
    }

    public void AddScore(float value)
    {
        _score.Add(value);
    }

    private void Tap()
    {
        _mover.Tap();
    }

    private void Shoot()
    {
        _shooter.Shoot();
    }

    private void Die()
    {
        Died?.Invoke();
    }
}