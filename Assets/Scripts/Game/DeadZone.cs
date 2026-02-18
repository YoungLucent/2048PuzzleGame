using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _timeToLose = 2.0f;

    private float _timer;
    private int _cubesInZoneCount;
    private bool _isGameOver;

    private void OnTriggerEnter(Collider other)
    {
        if (_isGameOver) return;

        if (other.attachedRigidbody != null && !other.attachedRigidbody.isKinematic)
        {
            if (other.GetComponent<CubeController>() != null)
            {
                _cubesInZoneCount++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isGameOver) return;

        if (other.attachedRigidbody != null && !other.attachedRigidbody.isKinematic)
        {
            if (other.GetComponent<CubeController>() != null)
            {
                if (_cubesInZoneCount > 0)
                {
                    _cubesInZoneCount--;
                }
            }
        }
    }

    private void Update()
    {
        if (_isGameOver) return;

        if (_cubesInZoneCount > 0)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeToLose)
            {
                TriggerGameOver();
            }
        }
        else
        {
            _timer = 0;
        }
    }

    private void TriggerGameOver()
    {
        _isGameOver = true;
        GameEvents.OnGameLose?.Invoke();
    }
}