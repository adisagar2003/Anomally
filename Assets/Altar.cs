using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] private GameObject altarSprite;
    [SerializeField] private GameObject KeyUI;
    bool isInRangeOfActivation = false;

    #region Sin Wave Controls
    [SerializeField] private float amplitude = 1;
    [SerializeField] private float frequency = 1;
    #endregion

    #region Events
    public delegate void InitiateEnemyManager();
    public static event InitiateEnemyManager StartEnemyManager;
    #endregion

    private void Start()
    {
        KeyUI.SetActive(false);
    }
    public void SetIsInRangeOfActivation(bool value)
    {
        isInRangeOfActivation = value;
    }
    
    private void Update()
    {
        LevitateSphere();
        TakeInput();
    }

    public void FixedUpdate()
    {

    }
    public void Initialize()
    {
        // Start the altar 
    }

    private void LevitateSphere()
    {
        float altarX = altarSprite.transform.position.x;
        float altarY = Mathf.Sin(Time.time * frequency) * amplitude;
        float altarZ = altarSprite.transform.position.z;
        altarSprite.transform.position = new Vector3(altarX, altarY, altarZ);
    }

    #region Collider Functionality
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetIsInRangeOfActivation(true);
            ShowUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetIsInRangeOfActivation(false);
            HideUI();         
        }
    }
    #endregion

    #region UI
    private void ShowUI()
    {
        if (isInRangeOfActivation)
        {
            KeyUI.SetActive(true);
        }
    }

    private void HideUI()
    {
        if (!isInRangeOfActivation) KeyUI.SetActive(false);
    }
    #endregion

    #region Input
    private void TakeInput()
    {
        if (isInRangeOfActivation && KeyUI.activeSelf) { 
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartEnemyManager();
                Destroy(gameObject);
            }
        }
    }
    #endregion
}

