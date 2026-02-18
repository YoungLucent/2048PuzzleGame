using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _scoreText;

    private int _currentScore;

    private void Start()
    {
        _currentScore = 0;
        UpdateScoreText();
    }

    private void OnEnable()
    {
        GameEvents.OnCubeMerged += AddScore;
    }

    private void OnDisable()
    {
        GameEvents.OnCubeMerged -= AddScore;
    }

    private void AddScore(int amount)
    {
        _currentScore += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"Score: {_currentScore}";
        }
    }
}
