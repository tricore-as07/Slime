using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeTMP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = default;
    [SerializeField, Range(0, 1f)] float minAlpha = 0;
    [SerializeField, Range(0, 1f)] float maxAlpha = 0;
    [SerializeField] float fadeTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        bool fadeOut = false;
        Color nowColor = default;
        while (true)
        {
            nowColor = text.color;
            if (fadeOut)
            {
                nowColor.a -= Time.deltaTime / fadeTime;
                text.color = nowColor;
                if(nowColor.a <= minAlpha)
                {
                    fadeOut = false;
                }
            }
            else
            {
                nowColor.a += Time.deltaTime / fadeTime;
                text.color = nowColor;
                if (nowColor.a >= maxAlpha)
                {
                    fadeOut = true;
                }
            }
            yield return null;
        }
    }
}
