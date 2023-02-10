using System;
using System.ComponentModel;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private PlayerManager playerManager;
    [SerializeField] private LevelManager LevelManager;
    [SerializeField] private CameraController camera;
    private GameState _gameState;
    
    private void Awake()
    {
        EventManager.GameFailed += OnGameFailed;
        EventManager.GameStartButtonClicked += GoMenuToGamePlay;
        _gameState = GameState.None;
    }
    
    private void CreateLevel()
    {
        LevelManager.PrepareCurrentLevel();
        playerManager = LevelManager.GetPlayerManager();
        camera.SetTarget(playerManager.GetPlayerTransform());
    }

    private void OnDestroy()
    {
        EventManager.GameFailed -= OnGameFailed;
        EventManager.GameStartButtonClicked -= GoMenuToGamePlay;
    }

    private void OnGameFailed()
    {
        Time.timeScale = 0;
        ChangeGameState(GameState.Finish);
    }
    
    private void Start()
    {   
        CreateLevel();
        
        ChangeGameState(GameState.GameStartMenu);
        Time.timeScale = 1;
    }

    private void ChangeGameState(GameState newState)
    {
        var oldState = _gameState;
        Debug.Log($"changing to state {oldState} - {newState}");
        _gameState = newState;
        EventManager.RaiseOnGameStateChanged(oldState, newState);
    }
    
    
    private void Update()
    {   
        
        
        if (_gameState == GameState.TapToStartMenu && SwipeManager.tap)
        {
            StartGamePlay();
        }
    }

    private void StartGamePlay()
    {
        ChangeGameState(GameState.Gameplay);
        playerManager.StartGame();
    }

    public void GoMenuToGamePlay()
    {
        ChangeGameState(GameState.TapToStartMenu);
    }
}
    
public enum GameState
{   
    None,
    GameStartMenu,
    TapToStartMenu,
    Gameplay,
    Finish,
}
