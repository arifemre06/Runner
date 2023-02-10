using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button startButton;
    void Start()
    {
        startButton = gameObject.GetComponent<Button>();
        startButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        EventManager.RaiseGameStartButtonClicked();
    }
    
    
    
    
}
