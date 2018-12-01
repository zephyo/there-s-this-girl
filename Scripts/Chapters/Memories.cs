using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;


public class Memories : MonoBehaviour
{

    public Sprite[] memories;
    public Image memory;
    int currMemIndex = 0;
    public CameraFilterPack_Blur_DitherOffset blur;



    public IEnumerator init()
    {   
        Image thisImg = transform.GetComponent<Image>();
        thisImg.raycastTarget = false;
        int currIndex = 0;

        blur.setDistance(Vector2.zero);

        blur.enabled = true;

        Color clear = new Color(1, 1, 1, 0), aim = new Color(1, 1, 1, 0.7f);

        float s = 1.5f;

        Vector2 dest = new Vector2(8, 2);

        float currTime = 0;

        LeanTween.value(gameObject, (float val) =>
      {
          if (val * 9 <= 1)
              memory.color = Color.Lerp(clear, aim, (val * 9) / 1);
          blur.setDistance(Vector2.Lerp(Vector2.zero, dest, val / 1));
      }, 0, 1, 5).setEaseInQuint().setOnComplete(() =>
        {
            memory.color = aim;
        });
        memory.sprite = memories[currIndex];
        yield return new WaitForSeconds(0.5f);
        while (currIndex < memories.Length)
        {
            memory.sprite = memories[currIndex];
            yield return new WaitForSeconds(s);
            currTime += s;
            s *= 0.87f;
            currIndex++;
        }

        LeanTween.value(gameObject, (float val) =>
        {
            memory.color = Color.Lerp(aim, clear, val / 1);
            blur.setDistance(Vector2.Lerp(dest, Vector2.zero, val / 1));
        }, 0, 1, 4).setEaseOutQuint().setOnComplete(() =>
        {
            memory.color = clear;
            thisImg.raycastTarget = true;
        });

    }


}
