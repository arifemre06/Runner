using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class GameWonPanel : UIPanel
    {
        [SerializeField] private Button nextLevel;
        [SerializeField] private Button mainMenuButton;


        private void Awake()
        {
            nextLevel.onClick.AddListener(OnClickNextLevelButton);
            mainMenuButton.onClick.AddListener(OnClickMainMenuButton);
        }

        private void OnClickNextLevelButton()
        {
            EventManager.RaiseNextLevel();
        }

        private void OnClickMainMenuButton()
        {
            EventManager.RaiseMainMenuButtonClicked();
        }
    }
}