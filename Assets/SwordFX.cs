using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFX : MonoBehaviour
{
    [Header("Destroy Object")]
    private float disappearTime = 0.2f;
    private void Start()
    {
        StartCoroutine(DisappearFX());
    }

    private IEnumerator DisappearFX()
    {
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        
    }
}
