using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("prototype");
    }

    public void Quit()
    {
      
    }
}
