using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;
using UnityEngine.EventSystems;

public class ScrollIn : MonoBehaviour
{

    public RectTransform[] scrollTransforms;
    // public int[] parallax;

    float[] goals;
    private int currIndex = 0;
    private float currTime = 0;

    private const float maxTime = 12f;

    private void Start()
    {
        goals = new float[scrollTransforms.Length];
        for (int i = 0; i < scrollTransforms.Length; i++)
        {
            goals[i] = scrollTransforms[i].anchoredPosition.x;
        }
        for (int i = 0; i < scrollTransforms.Length; i++)
        {
            RectTransform rt = scrollTransforms[i];
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x + rt.sizeDelta.x, rt.anchoredPosition.y);
        }
    }

    public void OnScroll(BaseEventData dat)
    {
        //if current scrollImage is fully in view
        PointerEventData p = (PointerEventData)dat;
        if (p.scrollDelta.x != 0)
        {
            t(p.scrollDelta.x);
        }
        else
        {
            t(p.scrollDelta.y);
        }
    }

    public void onDrag(BaseEventData dat)
    {
        PointerEventData p = (PointerEventData)dat;
        if (p.delta.x != 0)
        {
            t(p.delta.x);
        }
        else
        {
            t(p.delta.y);
        }
    }

    private void t(float delta)
    {
        RectTransform curr = scrollTransforms[currIndex];
        curr.anchoredPosition = new Vector2(Mathf.Lerp(curr.sizeDelta.x + goals[currIndex], goals[currIndex], currTime / maxTime), curr.anchoredPosition.y);

        currTime += Time.deltaTime * Mathf.Abs(delta);
        if (curr.anchoredPosition.x < goals[currIndex] + 3)
        {
            curr.anchoredPosition = new Vector2(goals[currIndex], curr.anchoredPosition.y);
            currIndex++;
            currTime = 0;
            if (currIndex >= scrollTransforms.Length)
            {
                Done();
            }
        }
    }

    void Done()
    {
        Destroy(GetComponent<EventTrigger>());
        StartCoroutine(enableScrollSnap());

    }

    IEnumerator enableScrollSnap()
    {
        yield return new WaitForSeconds(0.4f);
        DirectionalScrollSnap ds = transform.parent.parent.GetComponent<DirectionalScrollSnap>();
        ds.active= true;
        // yield return null;
        ds.forceStartScroll();
    }

}
