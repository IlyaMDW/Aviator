using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField]  private Image _imgPanel;
    [SerializeField] private Sprite _defaultPanel;
    [SerializeField] private Sprite _newRecordPanel;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _recordText;
    [SerializeField] private GameObject _defaultTitle;
    [SerializeField] private GameObject _newRecordTitle;

    private void OnEnable()
    {
        EventBus.playerDestroyed += OnPlayerDectroyed;
    }

    private void OnDisable()
    {
        EventBus.playerDestroyed -= OnPlayerDectroyed;
    }

    public void RestartButton()
    {
        EventBus.OnRestarted();
        _gameOverPanel.SetActive(false);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    private void OnPlayerDectroyed(int val, DataProvider dataProvider)
    {
        _gameOverPanel.SetActive(true);

        if (val > dataProvider.PlayerData._recordScore)
        {
            dataProvider.PlayerData._recordScore = val;
            dataProvider.SaveData<PlayerData>(dataProvider.PlayerData);
            _imgPanel.sprite = _newRecordPanel;

            _defaultTitle.SetActive(false);
            _newRecordTitle.SetActive(true);
        }
        else
        {
            _imgPanel.sprite = _defaultPanel;

            _defaultTitle.SetActive(true);
            _newRecordTitle.SetActive(false);
        }

        _scoreText.text = val.ToString();
        _recordText.text = dataProvider.PlayerData._recordScore.ToString();
    }
}
