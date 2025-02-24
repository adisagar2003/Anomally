using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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
