using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : MonoBehaviour, IInputProvider
{
    public event Action<float> OnDragDelta;
    public event Action OnTouchUp;

    [Header("Settings")]
    [SerializeField, Range(0.1f, 5f)] private float _sensitivity = 1.0f;

    private GameInput _gameInput;
    private bool _isTouching;

    private void Awake()
    {
        _gameInput = new GameInput();
    }

    private void OnEnable()
    {
        _gameInput.Enable();

        _gameInput.Gameplay.Press.started += OnPressStarted;
        _gameInput.Gameplay.Press.canceled += OnPressCanceled;
    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Press.started -= OnPressStarted;
        _gameInput.Gameplay.Press.canceled -= OnPressCanceled;
        _gameInput.Disable();
    }

    private void Update()
    {
        if (_isTouching)
        {
            ReadDrag();
        }
    }

    private void OnPressStarted(InputAction.CallbackContext context)
    {
        _isTouching = true;
    }

    private void OnPressCanceled(InputAction.CallbackContext context)
    {
        _isTouching = false;
        OnTouchUp?.Invoke();
    }

    private void ReadDrag()
    {
        Vector2 delta = _gameInput.Gameplay.Drag.ReadValue<Vector2>();

        float normalizedDelta = delta.x / Screen.width;

        if (Mathf.Abs(normalizedDelta) > Mathf.Epsilon)
        {
            OnDragDelta?.Invoke(normalizedDelta * _sensitivity);
        }
    }
}