using System.Collections;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CubeFactory _cubeFactory;

    [Header("Settings")]
    [SerializeField] private float _delayBeforeSpawn = 0.5f;

    private CubeController _activeCube;
    private bool _isGameActive = true;

    private void Start()
    {
        StartTurn();
    }

    private void OnEnable()
    {
        GameEvents.OnGameLose += HandleGameOver;
        GameEvents.OnGameWon += HandleGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnGameLose -= HandleGameOver;
        GameEvents.OnGameWon -= HandleGameOver;

        if (_activeCube != null)
        {
            _activeCube.OnCubeLaunched -= HandleCubeLaunched;
        }
    }

    private void StartTurn()
    {
        if (!_isGameActive) return;

        _activeCube = _cubeFactory.CreateCube();
        int value = _cubeFactory.GenerateValue();

        _activeCube.Initialize(_inputReader, value);

        _activeCube.OnCubeLaunched += HandleCubeLaunched;
    }

    private void HandleCubeLaunched()
    {
        if (_activeCube != null)
        {
            _activeCube.OnCubeLaunched -= HandleCubeLaunched;
            _activeCube = null;
        }

        if (_isGameActive)
        {
            StartCoroutine(WaitAndSpawnRoutine());
        }
    }

    private IEnumerator WaitAndSpawnRoutine()
    {
        yield return new WaitForSeconds(_delayBeforeSpawn);
        StartTurn();
    }

    private void HandleGameOver()
    {
        _isGameActive = false;

        StopAllCoroutines();

        if (_activeCube != null)
        {
            _activeCube.OnCubeLaunched -= HandleCubeLaunched;

            _activeCube.gameObject.SetActive(false); 

            _activeCube = null;
        }
    }
}