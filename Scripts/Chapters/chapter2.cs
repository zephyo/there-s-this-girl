using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;
using System;
public class chapter2 : Chapter
{


    public Sprite darkLibrary, stuffedBg, stars;
    public Sprite darkLibPpl;
    public GameObject exclamation, zz;


    public CameraFilterPack_Blur_Movie movie;
    public CameraFilterPack_Glow_Glow_Color bloomFx;

    IEnumerator activateEx()
    {
        yield return new WaitForSeconds(0.5f);
        exclamation.gameObject.SetActive(true);

        exclamation = null;
    }

    public override void onLastItem(int select)
    {
        base.onLastItem(select);

        if (select == 3)
        {
            FadeIn(() =>
            {
                transform.GetChild(0).GetComponent<Image>().sprite = darkLibrary;
                scrollSnap.snapChildren[select].GetChild(0).GetComponent<Image>().sprite = darkLibPpl;
                StartCoroutine(activateEx());
                zz.gameObject.SetActive(true);
                zz = null;
            });
        }
        else if (select == 5)
        {
            FadeInScene(() =>
            {
                scrollSnap.snapChildren[select].GetComponent<Image>().color = Color.white;
            });
        }
        else if (select == 6)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = stuffedBg;
        }
        else if (select == 10)
        {
            FadeInScene(() =>
            {
                CanvasGroup cg = scrollSnap.snapChildren[select].GetComponent<CanvasGroup>();
                cg.alpha = 1;
                cg.interactable = true;
                scrollSnap.snapChildren[select].GetComponent<Baseball>().enabled = true;
                scrollSnap.active = false;
            });
        }
        else if (select == 13)
        {
            scrollSnap.snapChildren[select].GetComponent<ScrollRect>().enabled = true;
            transform.GetChild(0).GetComponent<Image>().sprite = stars;
        }
        else if (select == 15)
        {
            transform.Find("p1").gameObject.SetActive(false);
            transform.Find("stars").gameObject.SetActive(true);
            FadeInScene(() =>
            {
                scrollSnap.snapChildren[select].GetComponent<Image>().color = Color.white;
            });
        }
        else if (select == 16)
        {
            scrollSnap.snapChildren[select].GetComponent<InteractiveStars>().init();
            scrollSnap.active = false;
        }
        else if (select == scrollSnap.snapChildren.Count - 2)
        {
            Image last = scrollSnap.snapChildren[select].GetComponent<Image>();
            last.color = Color.clear;
            transform.GetChild(1).gameObject.SetActive(true);
        }

    }


    protected void bloomandMovie(Action cb)
    {
        bloomFx.Threshold = 1;
        movie.Radius = 0;
        movie.enabled = true;
        bloomFx.enabled = true;
        LeanTween.value(gameObject, (float val) =>
        {
            movie.Radius = 350 - (350 * val);
            bloomFx.Threshold = val;
        }, 1, 0.1f, 1f).setOnComplete(() =>
        {
            if (cb != null)
            {
                cb();
            }
        });
    }

    protected void FadeInScene(Action cb)
    {
        canvas.blocksRaycasts = false;
        bloomandMovie(() =>
        {
            FadeIn(() =>
            {
                if (cb != null)
                {
                    cb();
                }
                movie.enabled = false;
                bloomFx.enabled = false;
            });
        });

    }




}
