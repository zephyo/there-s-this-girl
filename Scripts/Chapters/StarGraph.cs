using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System;

public class StarGraph : MonoBehaviour
{


    star currentlySelected;
    public List<UILineRenderer> lines;
    public event Action done;

    public CanvasGroup canvasGroup;

    public void init()
    {
        //fade in
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        transform.localScale = Vector3.zero;


        gameObject.SetActive(true);

        LeanTween.value(gameObject, (float val) =>
        {
            canvasGroup.alpha = val;
            transform.localScale = Vector3.one * val;
        }, 0, 1, 1f).setEaseInQuad().setOnComplete(() =>
                {
                    canvasGroup.alpha = 1;
                    transform.localScale = Vector3.one;
                    canvasGroup.blocksRaycasts = true;
                });
    }

    //button listener
    public void clickStar(star star)
    {
        if (currentlySelected == star)
        {
            currentlySelected.setNormal();
            currentlySelected = null;
        }

        //if currently selecting a star
        else if (currentlySelected != null)
        {
            if (currentlySelected.connectedTo.Contains(star))
            {
                return;
            }
            //connect two stars;
            connect(currentlySelected, star);

            //make both uninteractable; 
            // currentlySelected.GetComponent<Button>().interactable = false;
            // star.GetComponent<Button>().interactable = false;
            //set animator to connected; set currentlySelected = null
            currentlySelected.setConnected(star);
            star.setConnected(currentlySelected);
            currentlySelected = null;
        }
        else
        {
            //else, currentlySelected = star
            currentlySelected = star;
            currentlySelected.animator.SetTrigger("Pressed");
        }

    }

    private void connect(star one, star two)
    {
        UILineRenderer line = lines[0];
        line.Points = new Vector2[]{
            one.RectTransform.anchoredPosition,
            two.RectTransform.anchoredPosition
        };
        line.gameObject.SetActive(true);
        lines.RemoveAt(0);
        if (lines.Count == 0)
        {
            Done();
        }
    }

    private void Done()
    {
        //fade out

        canvasGroup.blocksRaycasts = false;
        LeanTween.value(gameObject, (float val) =>
        {
            canvasGroup.alpha = val;
            transform.localScale = Vector3.one * val;
        }, 1, 0, 1f).setDelay(1f).setEaseOutQuad().setOnComplete(() =>
        {
            canvasGroup.alpha = 0;
            transform.localScale = Vector3.zero;

            if (done != null)
                done();
        });

    }
}
