using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerPlane _player;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ItemSpawner _itemSpawner;
    [SerializeField] private PausePanel _pausePanel;

    private DataProvider _dataProvider;

    private async void Awake()
    {
        _dataProvider = new DataProvider();
        await _dataProvider.LoadDataAsync();
        GameManager.IsGameActive = true;

        _player.Initialize(_dataProvider);
        _enemySpawner.Initialize();
        _itemSpawner.Initialize();
        _pausePanel.Initialize(_dataProvider);
    }

    private void OnEnable()
    {
        EventBus.restarted += OnRestarted;
    }

    private void OnDisable()
    {
        EventBus.restarted -= OnRestarted;
    }

    private void OnRestarted()
    {
        GameManager.IsGameActive = true;
    }
}
