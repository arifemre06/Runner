using System;

namespace DefaultNamespace
{
    public static class EventManager
    {
        public static event Action<GameState, GameState> GameStateChanged;
        public static event Action GameFailed;
        public static event Action ArrivedToFinish;
        
        //todo rename these events
        public static event Action StartButtonClicked;
        public static event Action MainMenuButtonClicked;
        public static event Action RetryLevel;
        public static event Action NextLevel;
        public static event Action Quit;
        public static event Action TapToStart;
        
        

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
            StartButtonClicked?.Invoke();
        }

        public static void RaiseArrivedToFinish()
        {
            ArrivedToFinish?.Invoke();
        }

        public static void RaiseMainMenuButtonClicked()
        {
            MainMenuButtonClicked?.Invoke();
        }

        public static void RaiseRetryLevel()
        {
            RetryLevel?.Invoke();
        }

        public static void RaiseNextLevel()
        {
            NextLevel?.Invoke();
        }

        public static void RaiseQuit()
        {
            Quit?.Invoke();
        }

        public static void RaiseTapToStart()
        {
            TapToStart?.Invoke();
        }
    }
}