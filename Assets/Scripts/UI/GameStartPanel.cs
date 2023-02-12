using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class GameStartPanel : UIPanel
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button quitButton;
        
        private void Awake()
        {
            startButton.onClick.AddListener(OnClickStartButton);
            quitButton.onClick.AddListener(OnClickQuitButton);
        }

        private void OnClickStartButton()
        {
            EventManager.RaiseGameStartButtonClicked();
        }

        private void OnClickQuitButton()
        {
            EventManager.RaiseQuit(); }
    }
}