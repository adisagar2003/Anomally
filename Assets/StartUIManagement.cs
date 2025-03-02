using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIManagement : MonoBehaviour
{
    [SerializeField] private GameObject WelcomeScreen;
    [SerializeField] private GameObject StartPanel;
    private void OnEnable()
    {
        MoveToStartScreen.StartScreenEvent += ShowStartScreen;
    }

    private void ShowStartScreen()
    {
        WelcomeScreen.SetActive(false);
        StartPanel.SetActive(true);
    }


}
