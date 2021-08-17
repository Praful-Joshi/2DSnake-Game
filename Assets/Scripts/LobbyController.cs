using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public SnakeController snakeController;

    public void StartButton()
    {
        //code
        SceneManager.LoadScene(1);
        snakeController.ResetState();

    }

    public void QuitButton()
    {
        //code
        Application.Quit();
    }

    public void CoOpButton()
    {
        //code
        SceneManager.LoadScene(2);
    }

}
