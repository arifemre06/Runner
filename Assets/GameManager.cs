using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameOverBool;
    [SerializeField] private GameObject gameOverPanel;
    public static bool isGameStarted;
    [SerializeField] private GameObject gameStartText;
    void Start()
    {
        gameOverBool = false;
        Time.timeScale = 1;
        isGameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverBool)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(gameStartText);
        }
    }
    
}
