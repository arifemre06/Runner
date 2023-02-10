using System;

namespace DefaultNamespace
{
    public static class EventManager
    {
        public static event Action<GameState, GameState> GameStateChanged;
        public static event Action GameFailed;
        public static event Action GameStartButtonClicked;

        public static void RaiseOnGameStateChanged(GameState oldState, GameState newState)
        {
            GameStateChanged?.Invoke(oldState, newState);
        }

        public static void RaiseOnGameFailed()
        {
            GameFailed?.Invoke();
        }

        public static void RaiseGameStartButtonClicked()
        {
            GameStartButtonClicked?.Invoke();
        }
    }
}