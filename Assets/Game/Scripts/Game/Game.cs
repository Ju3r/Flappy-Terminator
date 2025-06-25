using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Alien _alien;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private StartWindow _startWindow;
    [SerializeField] private RestartWindow _restartWindow;

    private float _pauseValue = 0f;
    private float _playValue = 1f;

    private void OnEnable()
    {
        _alien.Died += StopPlay;
        _startWindow.PlayButtonClicked += StartPlay;
        _restartWindow.RestartButtonClicked += Restart;
        _enemySpawner.EnemyKilledByPlayer += AddPlayerScore;
    }

    private void OnDisable()
    {
        _alien.Died -= StopPlay;
        _startWindow.PlayButtonClicked -= StartPlay;
        _restartWindow.RestartButtonClicked -= Restart;
        _enemySpawner.EnemyKilledByPlayer += AddPlayerScore;
    }

    private void Start()
    {
        Time.timeScale = _pauseValue;
        _inputReader.Deactivate();
        _alien.DeactivateMover();
    }

    private void StopPlay()
    {
        _restartWindow.Show();
        Time.timeScale = _pauseValue;
        _inputReader.Deactivate();
    }

    private void StartPlay()
    {
        _startWindow.Hide();
        Play();
    }

    private void Restart()
    {
        _restartWindow.Hide();
        Play();
    }

    private void Play()
    {
        _alien.Reset();
        _enemySpawner.Reset();
        _bulletSpawner.Reset();
        _alien.ActivateMover();
        _inputReader.Activate();
        Time.timeScale = _playValue;
    }

    private void AddPlayerScore(float value)
    {
        _alien.AddScore(value);
    }
}