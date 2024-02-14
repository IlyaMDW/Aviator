using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private DataProvider _dataProvider;

    private int _currentScore;

    public int CurrentScore
    {
        get { return _currentScore; }
        set { _currentScore = value;
            _scoreText.text = _currentScore.ToString();
        }
    }

    public void Initialze(DataProvider dataProvider)
    {
        _currentScore = 0;
        _dataProvider = dataProvider;
        UpdateView();
    }

    private void OnEnable()
    {
        EventBus.enemyDestroyed += OnEnemyDestroyed;
    }

    private void OnDisable()
    {
        EventBus.enemyDestroyed -= OnEnemyDestroyed;
    }

    private void OnEnemyDestroyed(int val)
    {
        _currentScore += val;
        UpdateView();
    }

    private void UpdateView()
    {
        _scoreText.text = _currentScore.ToString();
    }
}
