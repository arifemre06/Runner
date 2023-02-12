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
        EventManager.StartButtonClicked += GoMenuToStartButtonClicked;
        EventManager.ArrivedToFinish += OnArrivedToFinish;
        EventManager.RetryLevel += OnRetry;
        EventManager.TapToStart += OnTapToStart;
        EventManager.NextLevel += OnNextLevel;
        EventManager.Quit += OnQuit;
        EventManager.MainMenuButtonClicked += OnMainMenuButtonClicked;
      
        _gameState = GameState.None;
    }

    private void OnDestroy()
    {
        EventManager.GameFailed -= OnGameFailed;
        EventManager.StartButtonClicked -= GoMenuToStartButtonClicked;
        EventManager.ArrivedToFinish -= IncreaseLevelIndexAndCreateNextLevel;
        EventManager.RetryLevel -= OnRetry;
        EventManager.TapToStart -= OnTapToStart;
        EventManager.NextLevel -= OnNextLevel;
        EventManager.Quit -= OnQuit;
        EventManager.MainMenuButtonClicked -= OnMainMenuButtonClicked;
    }

    private void OnArrivedToFinish()
    {
        ChangeGameState(GameState.GameWon);
    }

    private void OnMainMenuButtonClicked()
    {   
        
        CreateLevel();
        ChangeGameState(GameState.GameStartMenu);
        Time.timeScale = 1;
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnNextLevel()
    {
        IncreaseLevelIndexAndCreateNextLevel();
        ChangeGameState(GameState.TapToStartMenu);
    }

    private void OnTapToStart()
    {
        StartGamePlay();
    }

    private void OnRetry()
    {
        CreateLevel();
        ChangeGameState(GameState.TapToStartMenu);
    }

    private void IncreaseLevelIndexAndCreateNextLevel()
    {
        Debug.Log("buraya gelıyoz dımı emınız");
        levelManager.SetLevelIndexToNextLevel();
        CreateLevel();
    }

    private void CreateLevel()
    {
        levelManager.PrepareCurrentLevel();
        playerManager = levelManager.GetPlayerManager();
        camera.SetTarget(playerManager.transform);
    }

    private void OnGameFailed()
    {
        Time.timeScale = 0;
        ChangeGameState(GameState.GameFailed);
    }
    
    private void Start()
    {   
        OnMainMenuButtonClicked();
    }

    private void ChangeGameState(GameState newState)
    {
        var oldState = _gameState;
        Debug.Log($"changing to state {oldState} - {newState}");
        _gameState = newState;
        EventManager.RaiseOnGameStateChanged(oldState, newState);
    }
    
    private void StartGamePlay()
    {
        ChangeGameState(GameState.Gameplay);
        playerManager.StartGame();
    }

    public void GoMenuToStartButtonClicked()
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
    GameFailed,
    GameWon,
    
}
