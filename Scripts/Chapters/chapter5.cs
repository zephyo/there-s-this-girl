using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ScrollSnaps;
using UnityEngine.UI;
public class chapter5 : Chapter
{
    public CameraFilterPack_Glow_Glow_Color glow_Color;
    bool fading = false;
    public override void onLastItem(int select)
    {
        // if (select == 1)
        // {
        //     scrollSnap.snapChildren[select].GetComponent<Image>().raycastTarget = false;
        // }

        if (select == 1)
        {
            StartCoroutine(scrollSnap.snapChildren[select].GetComponent<Memories>().init());
        }

        else if (select == scrollSnap.snapChildren.Count - 1)
        {
            if (!fading)
            {
                fading = true;
                StartCoroutine(fade());
            }
        }
    }

    IEnumerator fade()
    {

        glow_Color.enabled = true;
        yield return new WaitForSeconds(1.5f);
        CanvasGroup cg = scrollSnap.snapChildren[scrollSnap.snapChildren.Count - 1].GetChild(1).GetComponent<CanvasGroup>();
        LeanTween.value(gameObject, (float val) =>
        {
            cg.alpha = val;
            glow_Color.setAmountThres(val * 2f, 0.8f - (0.3f * val));
        }, 0, 1, 4).setEaseInOutQuad().setOnComplete(() =>
        {

            StartCoroutine(theEnd());
        });
    }


}
