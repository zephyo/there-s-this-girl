using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.ScrollSnaps;

public class ScrollInMan : MonoBehaviour
{

    public DirectionalScrollSnap scrollSnap;

    public List<int> onScroll;

    public void ifOnScroll(int i)
    {
        if (onScroll.Contains(i))
        {
            onScroll.Remove(i);
            RectTransform snapChild = scrollSnap.snapChildren[i];
            ScrollIn s = snapChild.GetComponent<ScrollIn>();

            EventTrigger ET = snapChild.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Scroll;
            entry.callback.AddListener(s.OnScroll);
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.Drag;
            entry2.callback.AddListener(s.onDrag);
            ET.triggers.Add(entry);
            ET.triggers.Add(entry2);
            scrollSnap.active = false;
        }
    }
}
