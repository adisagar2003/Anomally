using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Heath_System : MonoBehaviour, IObserver
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.GetHealth();
    }

    public void InvokeEvent()
    {
        Debug.Log("Do something for UI");
        // Reduce the health..
        healthText.text = "It is hurt";

    }
}
