using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;
using TMPro;

public class Choose : MonoBehaviour
{

    public TextMeshProUGUI endText;
    public Image endImg, bg;
	public Sprite turnback, TBbg;
    public void TurnBack()
    {
		endImg.sprite = turnback;
        endImg.SetNativeSize();
        endText.text = "<size=150%><color=#ff6363>V</color></size>\nanother chance.";
		bg.sprite=TBbg;
        StartCoroutine(done());
    }

    public void MoveOn()
    {
        endImg.color = Color.clear;
        endText.text = "<size=150%><color=#ff6363>V</color></size>\nmove on.";
        StartCoroutine(done());
    }

    IEnumerator done()
    {
        yield return new WaitForSeconds(0.3f);
        DirectionalScrollSnap ds = transform.parent.parent.GetComponent<DirectionalScrollSnap>();
        ds.active= true;
        ds.ScrollToSnapPosition(transform.GetSiblingIndex()+1, 2, new Scroller.DecelerateInterpolator());
    }


}
