using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject DeathUI;
    // Start is called before the first frame update
    void Awake()
    {
        DeathUI.SetActive(false);
    }

    private void OnEnable()
    {
        Player.DeathEvent += ShowDeathUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowDeathUI()
    {
        DeathUI.SetActive(true);
    }
}

