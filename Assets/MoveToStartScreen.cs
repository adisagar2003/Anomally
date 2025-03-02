using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToStartScreen : MonoBehaviour
{
    public delegate void StartScreen();
    public static event StartScreen  StartScreenEvent;
    #region Time Control
    private float waitTime = 3.0f;
    private float timer = 0.0f;
    #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime && Input.GetKeyDown(KeyCode.A))
        {
            StartScreenEvent();
        }
    }
}
