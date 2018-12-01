using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;
using UnityEngine.EventSystems;
using System;
public class SpeechBubbles : MonoBehaviour
{

    public CanvasGroup[] bubbles;

    private int currentBubble = 0;

    public float speed = 14f;

    bool fading = false;
    bool addedSpeech = false;
    private void Start()
    {
        bubbles[currentBubble].transform.localScale = Vector2.zero;
        bubbles[currentBubble].gameObject.SetActive(true);

    }

    public void onScroll(BaseEventData dta)
    {
        t();
    }

    public void OnDrag(BaseEventData dat)
    {
        t();
    }

    void t()
    {
        if (fading) return;
        // if (p.scrollDelta.y > 0.2f)
        // {
        //     return;
        // }
        CanvasGroup curr = bubbles[currentBubble];

        curr.transform.localScale = Vector2.Lerp(curr.transform.localScale, Vector2.one, Time.deltaTime * speed);
        if (curr.transform.localScale.x > 0.99f)
        {

            fading = true;
            StartCoroutine(fade(curr));
        }
    }

    public void AddSpeech(int i)
    {

        if (i == 1)
        {
            if (addedSpeech) return;
            addedSpeech = true;
            EventTrigger ET = gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Scroll;
            entry.callback.AddListener(onScroll);
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.Drag;
            entry2.callback.AddListener(OnDrag);
            ET.triggers.Add(entry2);
            ET.triggers.Add(entry);
        }
    }

    IEnumerator fade(CanvasGroup curr)
    {
        RectTransform bbl = (RectTransform)curr.transform;
        float yPos = bbl.anchoredPosition.y;
        yield return new WaitForSeconds(0.2f);
        LeanTween.value(gameObject, (float val) =>
        {
            //plus 30, fade alpha
            bbl.anchoredPosition = new Vector2(bbl.anchoredPosition.x, yPos + 40 * val);
            curr.alpha = 1 - val;

        }, 0, 1, 1f).setEaseOutQuad().setOnComplete(() =>
        {
            bbl.gameObject.SetActive(false);
            currentBubble++;
            if (currentBubble >= bubbles.Length)
            {
                Done();

            }
            else
            {

                bubbles[currentBubble].transform.localScale = Vector2.zero;
                bubbles[currentBubble].gameObject.SetActive(true);
                fading = false;
            }
        });

    }

    void Done()
    {
        transform.parent.parent.GetComponent<DirectionalScrollSnap>().active = true;
        Destroy(GetComponent<EventTrigger>());
    }
}
