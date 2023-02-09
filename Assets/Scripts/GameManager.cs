using System;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    
    private GameState _gameState = GameState.Menu;
    
    private void Awake()
    {
        EventManager.GameFailed += OnGameFailed;
    }

    private void OnDestroy()
    {
        EventManager.GameFailed -= OnGameFailed;
    }

    private void OnGameFailed()
    {
        Time.timeScale = 0;
        ChangeGameState(GameState.Finish);
    }
    
    private void Start()
    {
        _gameState = GameState.Menu;
        Time.timeScale = 1;
    }

    private void ChangeGameState(GameState newState)
    {
        var oldState = _gameState;
        _gameState = newState;
        EventManager.RaiseOnGameStateChanged(oldState, newState);
    }
    
    

    private void Update()
    {
        if (_gameState == GameState.Menu && SwipeManager.tap)
        {
            StartGamePlay();
        }
    }

    private void StartGamePlay()
    {
        ChangeGameState(GameState.Gameplay);
        _playerManager.StartGame();
    }
}
    
public enum GameState
{
    Menu,
    Gameplay,
    Finish,
}
