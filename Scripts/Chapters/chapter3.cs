using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;
using UnityEngine.EventSystems;

public class chapter3 : Chapter
{


    public DirectionalScrollSnap firstSnap;


    public GameObject exclamation;
    public Image libraryBg;
    public Sprite darkLibrary;

    public CanvasGroup darkLibrarySprites;

    public CameraFilterPack_TV_Vignetting vignetting;

    public CameraFilterPack_Atmosphere_Rain rain;
    public CameraFilterPack_TV_BrokenGlass broken;






    public override void onLastItem(int select)
    {
        base.onLastItem(select);
        if (select == 1)
        {
            transform.Find("beg").gameObject.SetActive(false);
            transform.Find("angry").gameObject.SetActive(true);
        }
    }

    public void setExActive(int i)
    {
        if (i == 2)
        {
            exclamation.gameObject.SetActive(true);
        }
    }
    public void targetItemHorz(int i)
    {
        if (i == 5)
        {
            StartCoroutine(switchSprite());
        }
        else if (i == 8)
        {
            LeanTween.value(gameObject, (float val) =>
            {
                libraryBg.color = new Color(val, val, val, val);
            }, 1, 0, 1.5f).setEaseInOutQuad().setOnComplete(() =>
            {
                libraryBg.gameObject.SetActive(false);
            });
        }
    }

    IEnumerator switchSprite()
    {
        canvas.alpha = 0;
        libraryBg.sprite = darkLibrary;
        darkLibrarySprites.alpha = 1;
        yield return new WaitForSeconds(0.4f);
        canvas.alpha = 1;

    }
    public void lastRain(int i)
    {

        if (i == 8)
        {

            canvas.blocksRaycasts = false;
            Image im = scrollSnap.snapChildren[0].GetComponent<Image>();
            // GraphicRaycaster graphic = GetComponent<GraphicRaycaster>();
            rain.enabled = true;
            LeanTween.value(gameObject, (float val) =>
           {
               im.color = Color.Lerp(Color.black, Color.white, val / 0.6f);
               if (val < 0.56f)
                   rain.setFade(val);
           }, 0, 0.6f, 6f).setEaseInQuad().setOnComplete(() =>
            {
                canvas.blocksRaycasts = true;
            });
        }
    }
    public void FX(int i)
    {
        if (i == 1)
        {
            canvas.blocksRaycasts = false;
            LeanTween.value(gameObject, (float val) =>
            {
                rain.setFade(val);
            }, 0.548f, 0, 1.5f).setEaseOutQuad().setOnComplete(() =>
             {

                 rain.enabled = false;
                 canvas.blocksRaycasts = true;
             });
        }
        else if (i == 2)
        {
            scrollSnap.snapChildren[1].GetComponent<DriftApart>().OnDone();
        }
        else if (i == 3)
        {
            // GraphicRaycaster graphic = GetComponent<GraphicRaycaster>();
            canvas.blocksRaycasts = false;
            broken.enabled = true;
            LeanTween.value(gameObject, (float val) =>
           {
               broken.setSmall(val * 28.2f);
               broken.setMed(val * 35.3f);
               if (val > 0.1f)
               {
                   broken.setHigh((val - 0.1f) * 5.1f);
                   broken.setBig((val - 0.1f) * 51);
               }
           }, 0, 1, 10f).setEaseInOutQuad().setOnComplete(() =>
            {
                canvas.blocksRaycasts = true;
            });
        }
    }

    public void vignetteFX(int i)
    {

        // Debug.Log("target item " + i + " selected!");
        if (i == 1)
        {
            // graphic.enabled = false;

            vignetting.enabled = true;
            LeanTween.value(gameObject, (float val) =>
            {
                vignetting.setVignetting(val);
            }, 0, 0.4f, 1.5f).setEaseInQuad().setOnComplete(() =>
            {
            });
        }
        else if (i == 2)
        {

            LeanTween.value(gameObject, (float val) =>
            {
                vignetting.setVignetting(val);
            }, 0.4f, 0, 1).setEaseOutQuad().setOnComplete(() =>
             {

                 vignetting.enabled = false;

             });
        }
        else if (i == 3)
        {
            firstSnap.enabled = false;
        }
    }


    // public void Party1(Image party)
    // {

    // }

    // public void Party2(Image party)
    // {

    // }



}
