using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// TMPの文字をフェードアニメーションさせるクラス
/// </summary>
public class FadeTMP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = default;        //フェードさせるテキスト
    [SerializeField, Range(0, 1f)] float minAlpha = 0;      //最小のアルファ値
    [SerializeField, Range(0, 1f)] float maxAlpha = 0;      //最大のアルファ値
    [SerializeField] float fadeTime = 1f;                   //フェード時間

    /// <summary>
    /// オブジェクトがアクティブになった時に呼ばれる
    /// </summary>
    void OnEnable()
    {
        StartCoroutine(Fade());
    }

    /// <summary>
    /// フェード処理
    /// </summary>
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
