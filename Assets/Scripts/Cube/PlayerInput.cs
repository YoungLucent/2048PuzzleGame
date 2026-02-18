using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector2> OnDragging;
    public event Action OnFingerUp;

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Pointer.current == null) return;

        if (Pointer.current.press.isPressed)
        {
            Vector2 delta = Pointer.current.delta.ReadValue();
            OnDragging?.Invoke(delta);
        }

        if (Pointer.current.press.wasReleasedThisFrame)
        {
            OnFingerUp?.Invoke();
        }
    }
}