using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;

public class Baseball : MonoBehaviour
{

    private readonly Vector2 targetPan = new Vector2(-415, 789.4f);
    private const float targetY = 170, range = 35;
    private readonly Vector2 targetBaseball = new Vector2(-63, -496), startBaseball = new Vector2(1, 205), hitBaseball = new Vector2(93, 720);
    // private Vector2 leftBaseball;

    public RectTransform pan;
    public RectTransform baseball;
    public Animator hitter, pitcher;


    const float delay = 3.5f;
    float currTime = 1;


    bool hit = false;
    public void ButtonHit()
    {
        hitter.SetTrigger("hit");
        if (baseball.anchoredPosition.y > targetY - range && baseball.anchoredPosition.y < targetY + range)
        {
            // leftBaseball = baseball.anchored

            StartCoroutine(doneHit());
        }
    }

    private void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= delay)
        {
            pitcher.SetTrigger("throw");
            currTime = 0;
            baseball.gameObject.SetActive(true);
            baseball.localScale = Vector2.zero;
            baseball.anchoredPosition = startBaseball;
            LeanTween.value(gameObject, (float val) =>
            {
                //scale from  0.8766181 to 2.49786
                if (!hit)
                {
                    baseball.localScale = Vector3.one * Mathf.Lerp(0.87f, 2.49f, val / 1);
                    baseball.anchoredPosition = Vector2.Lerp(startBaseball, targetBaseball, val / 1);
                }
                else
                {
                    baseball.localScale = Vector3.one * Mathf.Lerp(baseball.localScale.x, 0.6f, Time.deltaTime);
                    baseball.anchoredPosition = Vector2.Lerp(baseball.anchoredPosition, hitBaseball, Time.deltaTime);
                }
            }, 0, 1, 1.3f).setDelay(0.5f).setEaseInQuad().setOnComplete(() =>
            {
                baseball.gameObject.SetActive(false);
            });
        }
    }

    IEnumerator doneHit()
    {
        enabled = false;
        yield return new WaitForSeconds(0.3f);
        hit = true;
        yield return new WaitForSeconds(0.7f);
        DirectionalScrollSnap ds = transform.parent.parent.GetComponent<DirectionalScrollSnap>();
        ds.active= true;
        ds.ScrollToSnapPosition(transform.GetSiblingIndex() + 1, 2, new Scroller.DecelerateInterpolator());
    }
    public void valuechanged(Vector2 value)
    {
        if (pan.anchoredPosition.x < targetPan.x + 5 && pan.anchoredPosition.y > targetPan.y - 5)
        {
            pan.anchoredPosition = targetPan;
            Image pImg = pan.GetComponent<Image>();
            pImg.raycastTarget = false;
            StartCoroutine(destroyScroll(pImg));
        }
    }

    IEnumerator destroyScroll(Image pImg)
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(pan.transform.parent.GetComponent<ScrollRect>());
        pImg.raycastTarget = true;
    }


}
