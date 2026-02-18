using System;
using UnityEngine;

[RequireComponent(typeof(CubeMovement))]
[RequireComponent(typeof(CubeMerge))]    
[RequireComponent(typeof(CubeEffects))]
[RequireComponent(typeof(CubeVisual))]
public class CubeController : MonoBehaviour
{
    private CubeMovement _movement;
    private CubeMerge _merge;           
    private CubeEffects _effects;
    private CubeVisual _visual;

    public int Value { get; private set; }
    public bool IsMerging { get; private set; }

    public event Action OnCubeLaunched;

    private void Awake()
    {
        _movement = GetComponent<CubeMovement>();
        _merge = GetComponent<CubeMerge>();
        _effects = GetComponent<CubeEffects>();
        _visual = GetComponent<CubeVisual>();
    }

    public void Initialize(IInputProvider inputProvider, int value)
    {
        Value = value;
        IsMerging = false;
        transform.localScale = Vector3.one;

        if (_visual != null) _visual.UpdateVisuals(Value);

        _movement.Initialize(inputProvider);
        _movement.OnLaunched += HandleLaunchEvent;

        _merge.Initialize(this);
    }

    private void OnDisable()
    {
        IsMerging = false;
        if (_movement != null)
        {
            _movement.Cleanup();
            _movement.OnLaunched -= HandleLaunchEvent;
        }
    }

    private void HandleLaunchEvent()
    {
        OnCubeLaunched?.Invoke();
    }

    public void StartMergeProcess()
    {
        IsMerging = true;
    }

    public void ApplyMergeSuccess()
    {
        GameEvents.OnCubeMerged?.Invoke(Value / 2);

        Value *= 2;

        if (Value == 2048)
        {
            GameEvents.OnGameWon?.Invoke();
        }

        if (_visual != null) _visual.UpdateVisuals(Value);

        _effects.PlayMergeEffect();

        IsMerging = false;
    }
}