using TMPro;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recordScore;

    private DataProvider _dataProvider;

    public void Initialize(DataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public void PauseButton()
    {
        if (gameObject.activeSelf)
            ContinuePlay();
        else
        {
            Time.timeScale = 0f;
            _recordScore.text = _dataProvider.PlayerData._recordScore.ToString();
            gameObject.SetActive(true);
        }
    }

    public void ContinuePlay()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
