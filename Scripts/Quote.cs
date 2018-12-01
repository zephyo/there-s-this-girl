using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quote : MonoBehaviour
{

    public TextMeshProUGUI text;
    public CanvasGroup canvasGroup;

    private CameraFilterPack_Glow_Glow_Color color;

    private const float finalGlow = 3, finalT = 0.8f;


    public void Init(string quote, float time, float wait)
    {
        Camera cam = Camera.main;
        GetComponent<Canvas>().worldCamera = cam;
        color = cam.gameObject.AddComponent<CameraFilterPack_Glow_Glow_Color>();
        StartCoroutine(type(quote, time, wait));
    }

    IEnumerator type(string quote, float time, float wait)
    {
        yield return new WaitForSeconds(wait);

        float maxTime = time - wait - 1.5f;
        float currTime = 0;

        float sec = Mathf.Clamp(maxTime / quote.Length, 0, 0.15f);
        WaitForSeconds s = new WaitForSeconds(sec);


        for (int i = 0; i < quote.Length; i++)
        {
            text.text += quote[i];
            if (quote[i] == '?' || quote[i] == ',')
            {
                // text.text+="\n";
                yield return new WaitForSeconds(0.8f);
            }
            yield return s;
            currTime += sec;
            color.setAmountThres(Mathf.Lerp(0, finalGlow, currTime / maxTime), Mathf.Lerp(finalT, 0.151f, currTime / maxTime));

        }
        text.text = quote;
    }
    public void FadeOut()
    {
        LeanTween.value(gameObject, (float val) =>
        {

            canvasGroup.alpha = val;
            color.setAmountThres(Mathf.Lerp(0, finalGlow, val), Mathf.Lerp(finalT, 0.151f, val));
        }, 1, 0, 1.5f).setEaseInOutQuad().setOnComplete(() =>
         {
             Destroy(gameObject);
             Destroy(color);
         });
    }
}
