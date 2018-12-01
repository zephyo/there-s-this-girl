using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;
using System;
using UnityEngine.EventSystems;
public class chapter1 : Chapter
{
    public CanvasGroup canvasGroup;
    public Image shake;


    public override void onLastItem(int select)
    {
        base.onLastItem(select);
        if (select == scrollSnap.snapChildren.Count - 2)
        {
            Image last = scrollSnap.snapChildren[select].GetComponent<Image>();
            last.color = Color.clear;
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (select == scrollSnap.snapChildren.Count - 3)
        {
            scrollSnap.snapChildren[select].GetComponent<InteractiveStars>().init();
            scrollSnap.active  = false;
        }
        else if (select == 2)
        {
            Image i = shake.transform.parent.GetComponent<Image>();
            CameraFilterPack_Blur_Steam steam = Camera.main.gameObject.AddComponent<CameraFilterPack_Blur_Steam>();
            i.raycastTarget = false;
            LeanTween.value(gameObject, (float val) =>
            {
                shake.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(150, 0, val), Mathf.Lerp(20, 0, val));
                shake.rectTransform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(10, 0, val));
                shake.color = new Color(1, 1, 1, val);
                steam.setRadius(Mathf.Lerp(0.2f, 0, val));
            }, 0, 1, 4).setEaseOutBounce().setOnComplete(() =>
            {
                shake.rectTransform.anchoredPosition = Vector2.zero;
                shake.color = Color.white;
                Destroy(steam);
                i.raycastTarget = true;
            });
        }
    }
    protected override IEnumerator fadeIn(AudioSource AS, int time, bool sameSong, Quote Q)
    {
        yield return new WaitForSeconds(quote.length - 0.3f);
        Q.FadeOut();
        yield return new WaitForSeconds(0.1f);
        AS.volume = 0;
        AS.enabled = false;
        AS.loop = true;
        AS.clip = song;

        if (sameSong)
        {
            AS.timeSamples = time;
        }

        AS.enabled = true;
        canvas.blocksRaycasts = false;
        LeanTween.value(gameObject, (float val) =>
        {
            canvas.alpha = val;
            AS.volume = val;
        }, 0, 1, 0.7f).setEaseInQuad().setOnComplete(() =>
        {
            canvas.blocksRaycasts = true;
        });


        LeanTween.value(gameObject, (float val) =>
        {

            canvasGroup.alpha = val;
        }, 0, 1, 0.5f).setEaseInOutQuad().setOnComplete(() =>
          {
              canvasGroup.alpha = 1;
          });
    }
}
