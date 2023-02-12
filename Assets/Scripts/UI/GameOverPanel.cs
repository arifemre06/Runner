using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class GameOverPanel : UIPanel
    {
        [SerializeField] private Button retryButton;
        [SerializeField] private Button mainMenuButton;


        private void Awake()
        {
            retryButton.onClick.AddListener(OnClickRetryButton);
            mainMenuButton.onClick.AddListener(OnClickMainMenuButton);
        }

        private void OnClickRetryButton()
        {
            EventManager.RaiseRetryLevel();
        }

        private void OnClickMainMenuButton()
        {
            EventManager.RaiseMainMenuButtonClicked();
        }
    }
}