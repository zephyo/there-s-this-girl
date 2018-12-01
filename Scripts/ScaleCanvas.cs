using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI.ScrollSnaps;
public class ScaleCanvas : MonoBehaviour
{

    public CanvasScaler scaler;

    public DirectionalScrollSnap[] scrollSnaps;

    float aspect;

    private void Awake()
    {
        aspect = (float)Screen.width / (float)Screen.height;
        if (aspect < 0.625f)
        {
            scaler.matchWidthOrHeight = 1;
        }
        else if (aspect > 0.625f)
        {
            scaler.matchWidthOrHeight = 0;
        }
    }
    private IEnumerator Start()
    {
        //640/1024 = 0.625
        //1080/1920 = 5625
        //3/4 = .75
        yield return null;

        RectTransform root = (RectTransform)transform;


        Vector2 offset = new Vector2((root.sizeDelta.x - 640) / 2,
        -((root.sizeDelta.y - 1024) / 2));

        if (aspect < 0.625f)
        {
            // Debug.Log(root.sizeDelta + " .. offset " + offset);
            foreach (DirectionalScrollSnap snap in scrollSnaps)
            {
                // if (snap.movementDirection != DirectionalScrollSnap.MovementDirection.Vertical)
                // {
                    snap.m_Offset = new Vector2(offset.x, 0);
                    snap.content.anchoredPosition += snap.m_Offset;
                // }
                // else
                // {
                //     snap.m_Offset = new Vector2(0, offset.y);
                //     snap.content.anchoredPosition += snap.m_Offset;
                // }
            }
        }
        else if (aspect > 0.625f)
        {
            foreach (DirectionalScrollSnap snap in scrollSnaps)
            {
                if (snap.movementDirection != DirectionalScrollSnap.MovementDirection.Vertical)
                {
                    snap.m_Offset = new Vector2(0, offset.y);
                    snap.content.anchoredPosition += snap.m_Offset;
                }
            }
        }
        Destroy(this);

    }

}
