using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class TapToStartPanel : UIPanel
    {
        [SerializeField] private Button tapToStartButton;
        
        
        private void Awake()
        {
            tapToStartButton.onClick.AddListener(OnClickTapToStartButton);
           
        }

        private void OnClickTapToStartButton()
        {
            EventManager.RaiseTapToStart();
        }
    }
}