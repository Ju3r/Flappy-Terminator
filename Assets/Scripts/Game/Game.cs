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
        _alien.Died += Stopping;
        _startWindow.PlayButtonClicked += Starting;
        _restartWindow.RestartButtonClicked += Restarting;
        _enemySpawner.EnemyKilledByPlayer += AddPlayerScore;
    }

    private void OnDisable()
    {
        _alien.Died -= Stopping;
        _startWindow.PlayButtonClicked -= Starting;
        _restartWindow.RestartButtonClicked -= Restarting;
        _enemySpawner.EnemyKilledByPlayer += AddPlayerScore;
    }

    private void Start()
    {
        Time.timeScale = _pauseValue;
        _inputReader.Deactivate();
        _alien.DeactivateMover();
    }

    private void Stopping()
    {
        _restartWindow.Show();
        Time.timeScale = _pauseValue;
        _inputReader.Deactivate();
    }

    private void Starting()
    {
        _startWindow.Hide();
        Play();
    }

    private void Restarting()
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