using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject gameOverPanel;
    public SnakeController snakeController;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        startMenuPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOverPanel()
    {
        snakeController.ResetState();
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void StartButton()
    {
        snakeController.ResetState();
        startMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitButton()
    {
        //code
        Application.Quit();
    }

    public void RestartButton()
    {
        //code
        gameOverPanel.SetActive(false);
        snakeController.ResetState();
        Time.timeScale = 1f;
    }
}
