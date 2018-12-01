using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI.ScrollSnaps;

public class Walk : MonoBehaviour
{


    public Image sporty, sleepy;

    private readonly Vector2 dest = new Vector2(-118.5f, 530), sleepyDest = new Vector2(95, 414);
    private Vector2 sleepyStart, sportyStart;
    float speed = 0.5f;
    float currTime = 0;

    private readonly Color clear = new Color(0.5f, 0.4f, 0.4f, 0), clear2 = new Color(0.7f, 0.7f, 0.7f, 0.3f);

    private void Start()
    {
        sleepyStart = sleepy.rectTransform.anchoredPosition;
        sportyStart = sporty.rectTransform.anchoredPosition;
    }

    public void OnScroll(BaseEventData dat)
    {
        t();
    }

    public void OnDrag(BaseEventData dat)
    {
        t();
    }
    void t()
    {
        float t = currTime / 1.5f;
        sporty.color = Color.Lerp(clear, Color.white, t * 2);
        sleepy.color = Color.Lerp(clear2, Color.white, t * 2);
        sporty.rectTransform.anchoredPosition = Vector2.Lerp(sportyStart, dest, t);
        sleepy.rectTransform.anchoredPosition = Vector2.Lerp(sleepyStart, sleepyDest, t);
        sleepy.transform.localScale = Vector2.one * Mathf.Lerp(1, 1.2f, t);
        sporty.transform.localScale = Vector2.one * Mathf.Lerp(1, 0.95f, t);
        if (sporty.rectTransform.anchoredPosition.y > dest.y - 3f && sleepy.rectTransform.anchoredPosition.y < sleepyDest.y + 3)
        {
            Done();
        }
        currTime += Time.deltaTime * speed;
    }

    void Done()
    {
        DirectionalScrollSnap ds = transform.parent.parent.GetComponent<DirectionalScrollSnap>();
        Destroy(GetComponent<EventTrigger>());
        ds.enabled = true;
        // GetComponent<Image>().raycastTarget = false;
        // ds.ScrollToSna/pPosition(1, 1f, new Scroller.DecelerateInterpolator());
    }
}
