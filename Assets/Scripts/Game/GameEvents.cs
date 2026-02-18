using System;

public static class GameEvents
{
    public static Action<int> OnCubeMerged;
    public static Action OnGameLose;
    public static Action OnGameWon;
}