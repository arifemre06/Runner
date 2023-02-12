using System;
using DefaultNamespace.UI;
using UnityEngine;

namespace DefaultNamespace
{   
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIPanel gameStartPanel;
        [SerializeField] private UIPanel  gameOverPanel;
        [SerializeField] private UIPanel  tapToStartPanel;
        [SerializeField] private UIPanel  gameWonPanel;
        
        private void Awake()
        {
            EventManager.GameStateChanged += OnGameStateChanged;
            DeActivateAllPanels();
            
        }

        private void OnDestroy()
        {
            EventManager.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState oldState, GameState newState)
        {
            DeActivateAllPanels();
            if (newState == GameState.GameStartMenu)
            {   
                
                ActivateGameStartPanel();
            }
            else if (newState == GameState.TapToStartMenu)
            {   
               
                ActivateTapToStartPanel();
                
            }
            else if (newState == GameState.Gameplay)
            {
                
            }
            else if (newState == GameState.GameFailed)
            {   
                
                ActivateGameOverPanel();
            }
            else if (newState == GameState.GameWon)
            {   
                
                ActivateGameWonPanel();
            }
        }

        private void DeActivateAllPanels()
        {
            tapToStartPanel.DeActivatePanel();
            gameStartPanel.DeActivatePanel();
            gameOverPanel.DeActivatePanel();
            gameWonPanel.DeActivatePanel();
        }


        private void ActivateGameStartPanel()
        {
            gameStartPanel.ActivatePanel();
        }
        private void ActivateTapToStartPanel()
        {
            tapToStartPanel.ActivatePanel();
        }

        private void ActivateGameOverPanel()
        {
            gameOverPanel.ActivatePanel();
        }

        private void ActivateGameWonPanel()
        {
            gameWonPanel.ActivatePanel();
        }
    }
}