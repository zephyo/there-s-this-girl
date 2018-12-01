using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ScrollSnaps;
using UnityEngine.EventSystems;

public class InteractiveStars : MonoBehaviour
{

    public StarGraph[] starGraphs;
    private void Awake()
    {
        for (int i = 0; i < starGraphs.Length - 1; i++)
        {
            int j = i+1;
            starGraphs[i].done += () => starGraphs[j].init();
        }
        starGraphs[starGraphs.Length - 1].done += done;
    }

    public void init()
    {
        starGraphs[0].init();
    }
    private void done()
    {
        DirectionalScrollSnap ds = transform.parent.parent.GetComponent<DirectionalScrollSnap>();
        ds.active= true;
        ds.ScrollToSnapPosition(transform.GetSiblingIndex() + 1, 1f, new Scroller.DecelerateInterpolator());
    }

}
