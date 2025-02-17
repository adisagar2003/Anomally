using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float maxWidth = 300.0f;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        Player.PlayerDamageEvent += TakeDamage;
        
    }

    private void OnDisable()
    {
        Player.PlayerDamageEvent -= TakeDamage;
    }

    private void TakeDamage(float damage)
    {
        maxWidth -= damage * 3;
        rectTransform.sizeDelta = new Vector2(Mathf.Clamp(maxWidth,0,300.0f), rectTransform.sizeDelta.y);
    }

}
