using System;
using UnityEngine;

namespace DefaultNamespace
{   
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject gameStartText;

        private void Awake()
        {
            EventManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            EventManager.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState oldState, GameState newState)
        {
            if (newState == GameState.Menu)
            {
                ActivateStartText(true);
                ActivateGameOverPanel(false);
            }
            else if (newState == GameState.Gameplay)
            {
                ActivateStartText(false);
                ActivateGameOverPanel(false);
            }
            else if (newState == GameState.Finish)
            {
                ActivateGameOverPanel(true);
            }
        }


        private void ActivateStartText(bool isActive)
        {
            gameStartText.SetActive(isActive);
        }

        private void ActivateGameOverPanel(bool isActive)
        {
            gameOverPanel.SetActive(isActive);
        }
    }
}