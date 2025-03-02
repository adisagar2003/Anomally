using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [ContextMenu("Start")]
    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
    [ContextMenu("Retry")]
    public void Retry()
    {
        SceneManager.LoadScene("MainLevel");
    }

    [ContextMenu("Quit")]
    public void Quit()
    {
        Application.Quit();
    }
}
