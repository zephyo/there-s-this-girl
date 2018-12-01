using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FadePointer : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    bool fade = false;
    public void OnBeginDrag(BaseEventData eventData)
    {
        if (fade) return;
        fade = true;
		Destroy(GetComponent<EventTrigger>());
        LeanTween.value(gameObject, (float val) =>
        {
            canvasGroup.alpha = val;
        }, 1, 0, 0.8f).setEaseInOutQuad().setOnComplete(() =>
        {
            canvasGroup.alpha = 0;
			canvasGroup.gameObject.SetActive(false);
        });
    }
}
