using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// blink "Press any key to start"
public class BlinkText : MonoBehaviour
{
    private Image image;
    [SerializeField] private float blinkTimeSpeed = 1.5f;
    private float timer = 0;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        SetOpacityWithRespectToTime();

    }

    private void SetOpacityWithRespectToTime()
    {
        timer += Time.deltaTime;
        if (timer > 3.0f)
        {
            var tempColor = image.color;
            tempColor.a = Mathf.PingPong(Time.time * blinkTimeSpeed, 1);
            image.color = tempColor;
        }
    }
}
