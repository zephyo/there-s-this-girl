using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
public class DriftApart : MonoBehaviour
{

    public CameraFilterPack_Distortion_FishEye fishEye;
    public SpeechBubbles speech;
    float speed = 0.0001f;

    public Image left, right;

    readonly Vector2 leftDest = new Vector2(-188.01f, -208), rightDest = new Vector2(59, -139.5f);

    readonly Color fade = new Color(1, 1, 1, 0.2f);

    bool addedSpeech = false;

    public void AddDrift(int i)
    {

        if (i == 1)
        {
            if (addedSpeech) return;
            addedSpeech = true;
            EventTrigger ET = gameObject.GetComponent<EventTrigger>();
            EventTrigger.Entry ent = ET.triggers.FirstOrDefault(entry => entry.eventID == EventTriggerType.Scroll);
            ent.callback.AddListener(onScroll);
            EventTrigger.Entry ent2 = ET.triggers.FirstOrDefault(entry => entry.eventID == EventTriggerType.Drag);
            ent2.callback.AddListener(OnDrag);
            fishEye.enabled = true;
            fishEye.setDistort(0.5f);
        }
    }

    public void onScroll(BaseEventData dta)
    {
        // PointerEventData p = (PointerEventData)dta;
        // if (p.scrollDelta.y > 0.2f)
        // {
        //     return;
        // }
        t();
    }

    public void OnDrag(BaseEventData dat)
    {
        t();
    }
    private void t()
    {
        float t = Time.deltaTime * speed;
        left.rectTransform.anchoredPosition = Vector2.Lerp(left.rectTransform.anchoredPosition, leftDest, t);
        left.color = Color.Lerp(left.color, fade, t);

        right.rectTransform.anchoredPosition = Vector2.Lerp(right.rectTransform.anchoredPosition, rightDest, t);
        right.color = Color.Lerp(right.color, fade, t);
        fishEye.setDistort(Mathf.Lerp(fishEye.Distortion, 0.763f, t / 2));

        speech.speed += 0.1f;
        speed += 0.001f;
    }


    public void OnDone()
    {
        if (!fishEye.enabled) return;
        float orig = fishEye.Distortion;
        LeanTween.value(gameObject, (float val) =>
        {
            fishEye.setDistort(val);
        }, orig, 0.5f, 0.2f).setEaseOutQuad().setOnComplete(() =>
        {
            fishEye.enabled = false;
        });
    }
}
