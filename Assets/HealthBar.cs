using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float initialWidth;
    private float maxWidth;
    private RectTransform rectTransform;
    private float playerHealth = 100.0f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        maxWidth = rectTransform.rect.width;
        initialWidth = rectTransform.rect.width;
    }

    private void OnEnable()
    {
        Player.OnSendCurrentHealth += SetPlayerHealth;       
        Player.PlayerDamageEvent += TakeDamage;
    }

    private void OnDisable()
    {
        Player.OnSendCurrentHealth -= SetPlayerHealth;
        Player.PlayerDamageEvent -= TakeDamage;

    }

    private void TakeDamage(float damage)
    {
        playerHealth -= damage;
        rectTransform.sizeDelta = new Vector2(Mathf.Clamp(SetWidthForHealthBar(),0,300.0f), rectTransform.sizeDelta.y);
    }

    private float SetWidthForHealthBar()
    {
        return (playerHealth / 100.0f) * maxWidth;
    }

    private void SetPlayerHealth(float health)
    {
        playerHealth = health;
    }

}
