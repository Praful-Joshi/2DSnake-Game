using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject gameOverPanel;
    public SnakeController snakeController;

    // Start is called before the first frame update
    void Start()
    {
        snakeController.ResetState();
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
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

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
