using System;
using System.ComponentModel;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private PlayerManager playerManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private CameraController camera;
    private GameState _gameState;
    
    private void Awake()
    {
        EventManager.GameFailed += OnGameFailed;
        EventManager.GameStartButtonClicked += GoMenuToGamePlay;
        EventManager.ArrivedToFinish += FinishCompletedLevel;
        _gameState = GameState.None;
    }

    private void FinishCompletedLevel()
    {
        levelManager.SetLevelIndexToNextLevel();
        CreateLevel();
    }

    private void CreateLevel()
    {
        levelManager.PrepareCurrentLevel();
        playerManager = levelManager.GetPlayerManager();
        Debug.Log("buraya gelmıyoz mu simdi");
        camera.SetTarget(playerManager.GetPlayerTransform());
    }

    private void OnDestroy()
    {
        EventManager.GameFailed -= OnGameFailed;
        EventManager.GameStartButtonClicked -= GoMenuToGamePlay;
        EventManager.ArrivedToFinish -= FinishCompletedLevel;
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
