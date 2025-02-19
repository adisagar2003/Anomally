using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void Retry()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
