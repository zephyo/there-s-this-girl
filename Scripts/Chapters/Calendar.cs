using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Calendar : MonoBehaviour
{

    public TextMeshProUGUI text;
    public Image button;
    public CanvasGroup calendar;

    public void one(chapter4 root)
    {
        text.text = "october\n<size=80%>" + DateTime.Now.Year;
        //days go by
        slideIn(root.canvas, () =>
        {
            StartCoroutine(daysGoBy(root));
        });
    }
    IEnumerator daysGoBy(chapter4 root)
    {
        float sec = 0.3f;
        for (int i = 4; i < 17; i++)
        {
            int row = i / 7;
            int column = (i % 7) - 3;
            button.rectTransform.anchoredPosition = new Vector2(column * 33.5f + 14f, 44.5f - 39 * row);
            yield return new WaitForSeconds(sec);
            sec *= 0.85f;
        }
        yield return new WaitForSeconds(1);
        slideOut(root.canvas);
        root.scrollSnap.ScrollToSnapPosition(1, 1, null);
    }


    public void two(chapter4 root)
    {
        // 2 months go by
        slideIn(root.canvas, () =>
        {
            StartCoroutine(monthsGoBy(root));
        });
    }
    IEnumerator monthsGoBy(chapter4 root)
    {
        float sec = 0.3f;

        int year = DateTime.Now.Year;

        for (int i = 18; i < 7 * 5; i++)
        {
            int row = i / 7;
            int column = (i % 7) - 3;
            button.rectTransform.anchoredPosition = new Vector2(column * 33.5f + 14f, 44.5f - 39 * row);
            yield return new WaitForSeconds(sec);
            sec *= 0.92f;
        }
        WaitForSeconds ws = new WaitForSeconds(sec);
        yield return ws;
        text.text = "november\n<size=80%>" + year;
        for (int i = 4; i < 7 * 5; i++)
        {
            int row = i / 7;
            int column = (i % 7) - 3;
            button.rectTransform.anchoredPosition = new Vector2(column * 33.5f + 14f, 44.5f - 39 * row);
            yield return ws;
        }
        yield return ws;
        text.text = "december\n<size=80%>" + year;
        for (int i = 4; i < 12; i++)
        {
            int row = i / 7;
            int column = (i % 7) - 3;
            button.rectTransform.anchoredPosition = new Vector2(column * 33.5f + 14f, 44.5f - 39 * row);
            yield return ws;
        }
        yield return new WaitForSeconds(0.5f);
        slideOut(root.canvas);
        root.scrollSnap.ScrollToSnapPosition(4, 1, null);
    }

    public void three(chapter4 root)
    {
        //year goes by
        slideIn(root.canvas, () =>
        {
            StartCoroutine(yearGoesBy(root));
        });
    }
    IEnumerator yearGoesBy(chapter4 root)
    {
        WaitForSeconds Longws = new WaitForSeconds(0.7f);
        yield return Longws;

        int year = DateTime.Now.Year + 1;

        string[] months = new string[] {
            "January",
            "February",
            "march",
            "april",
            "may",
            "june",
         };

        int i;

        for (int j = 0; j < months.Length; j++)
        {
            i = UnityEngine.Random.Range(4, 7 * 5);
            int row = i / 7;
            int column = (i % 7) - 3;
            button.rectTransform.anchoredPosition = new Vector2(column * 33.5f + 14f, 44.5f - 39 * row);
            text.text = months[j] + "\n<size=80%>" + year;
            yield return Longws;

        }
        yield return Longws;
        slideOut(root.canvas);
        root.scrollSnap.ScrollToSnapPosition(7, 1, null);


   
    }


    public void slideIn(CanvasGroup c, Action onComplete)
    {
        c.blocksRaycasts = false;
        RectTransform rt = (RectTransform)transform;
        calendar.alpha = 0;
        gameObject.SetActive(true);
        LeanTween.value(gameObject, (float val) =>
        {
            rt.anchoredPosition = new Vector2(0, 280 - val * 280);
            rt.eulerAngles = new Vector3(-30 + 30 * val, 0, 0);
            calendar.alpha = val;
        }, 0, 1, 2f).setEaseInQuad().setOnComplete(() =>
        {
            rt.anchoredPosition = Vector2.zero;
            rt.eulerAngles = Vector3.zero;
            calendar.alpha = 1;
            onComplete();
        });
    }

    public void slideOut(CanvasGroup c)
    {
        RectTransform rt = (RectTransform)transform;
        LeanTween.value(gameObject, (float val) =>
        {
            rt.anchoredPosition = new Vector2(0, val * -486);
            rt.eulerAngles = new Vector3(7.05f * val, 0, 0);
            calendar.alpha = 1 - val;
        }, 0, 1, 1.5f).setEaseOutQuad().setOnComplete(() =>
         {
             rt.anchoredPosition = new Vector2(0, -486);
             rt.eulerAngles = new Vector3(7.05f, 0, 0);
             calendar.alpha = 0;
             c.blocksRaycasts = true;
                  gameObject.SetActive(false);
         });
    }
}
