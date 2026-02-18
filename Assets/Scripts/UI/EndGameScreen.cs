using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndGameScreen : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameEndType _screenType;

    [Header("UI Elements")]
    [SerializeField] private Button _restartButton;
    private CanvasGroup _canvasGroup;

    public enum GameEndType
    {
        Lose, 
        Win     
    }
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        _restartButton.onClick.AddListener(RestartGame);
    }

    private void OnEnable()
    {
        switch (_screenType)
        {
            case GameEndType.Lose:
                GameEvents.OnGameLose += Show;
                break;
            case GameEndType.Win:
                GameEvents.OnGameWon += Show;
                break;
        }
    }

    private void OnDisable()
    {
        GameEvents.OnGameLose -= Show;
        GameEvents.OnGameWon -= Show;
    }

    private void Show()
    {
        _canvasGroup.DOFade(1f, 0.5f)
            .OnComplete(() =>
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            });
    }

    private void RestartGame()
    {
        _restartButton.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 10, 1)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
    }
}