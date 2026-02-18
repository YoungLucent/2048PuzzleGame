using System;

public interface IInputProvider
{
    event Action<float> OnDragDelta;
    event Action OnTouchUp;
}
