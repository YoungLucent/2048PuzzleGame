using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : MonoBehaviour
{
    public event Action OnLaunched;

    [Header("Settings")]
    [SerializeField] private float _horizontalSpeed = 20f;
    [SerializeField] private float _boundaryX = 4f;
    [SerializeField] private float _launchForce = 15f;

    private Rigidbody _rb;
    private IInputProvider _inputProvider;
    private bool _hasFired;

    public float VelocityMagnitude => _rb.linearVelocity.magnitude;

    public void Initialize(IInputProvider inputProvider)
    {
        _rb = GetComponent<Rigidbody>();
        _inputProvider = inputProvider;
        _hasFired = false;

        _rb.isKinematic = false;
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.isKinematic = true;

        Subscribe();
    }

    public void Cleanup()
    {
        Unsubscribe();
    }

    public void StopMovement()
    {
        _rb.isKinematic = true;
    }

    private void Subscribe()
    {
        if (_inputProvider != null)
        {
            _inputProvider.OnDragDelta += HandleDrag;
            _inputProvider.OnTouchUp += HandleLaunch;
        }
    }

    private void Unsubscribe()
    {
        if (_inputProvider != null)
        {
            _inputProvider.OnDragDelta -= HandleDrag;
            _inputProvider.OnTouchUp -= HandleLaunch;
        }
    }

    private void HandleDrag(float deltaX)
    {
        if (_hasFired) return;

        Vector3 newPos = _rb.position;
        newPos.x += deltaX * _horizontalSpeed;
        newPos.x = Mathf.Clamp(newPos.x, -_boundaryX, _boundaryX);

        _rb.MovePosition(newPos);
    }

    private void HandleLaunch()
    {
        if (_hasFired) return;
        _hasFired = true;

        _rb.isKinematic = false;
        _rb.linearVelocity = new Vector3(0, 0, _launchForce);
        _rb.angularVelocity = Vector3.zero;

        OnLaunched?.Invoke();

        Unsubscribe();
    }
}