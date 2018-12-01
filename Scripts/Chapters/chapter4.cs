using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class chapter4 : Chapter
{


    public Image left, right;
    public CameraFilterPack_Glow_Glow_Color glow_Color;


    public Calendar c;
    public VerticalLayoutGroup group;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(groupp());
    }

    IEnumerator groupp()
    {
        yield return null;
        yield return null;
        group.enabled = true;
        yield return null;
        group.enabled = false;
    }

    protected override IEnumerator fadeIn(AudioSource AS, int time, bool sameSong, Quote Q)
    {
        yield return new WaitForSeconds(quote.length - 0.3f);
        Q.FadeOut();
        yield return new WaitForSeconds(0.1f);
        AS.volume = 0;
        AS.loop = true;
        AS.clip = song;

        if (sameSong)
        {
            AS.timeSamples = time;
        }

        AS.Play();
        canvas.blocksRaycasts = false;
        LeanTween.value(gameObject, (float val) =>
        {
            canvas.alpha = val;
            AS.volume = val;
        }, 0, 1, 0.7f).setEaseInQuad().setOnComplete(() =>
        {
            c.one(this);
        });
    }


    public override void onLastItem(int select)
    {
        base.onLastItem(select);
        if (select == 0 || select > 8) return;

        if (select == 3)
        {
            c.two(this);
        }
        else if (select == 6)
        {
            c.three(this);
        }
        else
        {
            Image child = scrollSnap.transform.GetChild(0).GetChild(select).GetChild(0).GetComponent<Image>();
            canvas.blocksRaycasts = false;

            switch (select)
            {
                //set left
                case 1:
                    left.sprite = child.sprite;
                    _2dxFX_realVHS vhs = left.GetComponent<_2dxFX_realVHS>();
                    vhs.enabled = true;
                    LeanTween.value(gameObject, (float val) =>
                   {
                       vhs.changeParam(val * 0.21f, val * 0.634f, val * 1, 1 + val * 0.2f);
                   }, 0, 1, 1f).setEaseInOutQuad().setOnComplete(() =>
                   {
                       canvas.blocksRaycasts = true;
                   });

                    break;
                case 4:
                    left.sprite = child.sprite;
                    Destroy(left.GetComponent<_2dxFX_realVHS>());
                    _2DxFX_BrokenScreen bs = left.GetComponent<_2DxFX_BrokenScreen>();
                    bs.enabled = true;

                    LeanTween.value(gameObject, (float val) =>
                 {
                     bs.setFade(val * 0.2f);
                 }, 0, 1, 3f).setEaseInOutQuad().setOnComplete(() =>
                 {
                     canvas.blocksRaycasts = true;
                 });
                    break;
                case 7:
                    left.sprite = child.sprite;
                    Destroy(left.GetComponent<_2DxFX_BrokenScreen>());
                    // glow_Color.enabled = true;
                    _2dxFX_Liquify liq2 = right.GetComponent<_2dxFX_Liquify>();
                    LeanTween.value(gameObject, (float val) =>
                 {
                     //  glow_Color.setAmountThres(val * 2.27f, 1 - (val * 0.5f));
                     liq2.setAlpha(1 - val);
                     liq2.setTurnToLiquid(val + 0.02f);
                 }, 0, 1, 4f).setEaseInOutQuad().setOnComplete(() =>
                 {
                     right.color = Color.clear;
                     Destroy(liq2);
                     canvas.blocksRaycasts = true;
                 });
                    break;

                //set right
                case 2:
                    right.sprite = child.sprite;
                    _2dxFX_Rain rain = right.GetComponent<_2dxFX_Rain>();
                    rain.enabled = true;
                    LeanTween.value(gameObject, (float val) =>
                 {
                     rain.setFade(val);
                 }, 0, 1, 2f).setEaseInOutQuad().setOnComplete(() =>
                 {
                     canvas.blocksRaycasts = true;
                 });
                    break;
                case 5:
                    right.sprite = child.sprite;
                    Destroy(right.GetComponent<_2dxFX_Rain>());
                    _2dxFX_Liquify liq = right.GetComponent<_2dxFX_Liquify>();
                    liq.enabled = true;
                    LeanTween.value(gameObject, (float val) =>
                {
                    liq.setAlpha(val);
                    liq.setTurnToLiquid(val * 0.02f);
                }, 0, 1, 2.5f).setEaseInOutQuad().setOnComplete(() =>
                 {
                     canvas.blocksRaycasts = true;
                 });
                    break;
                case 8:
                    right.sprite = child.sprite;
                    right.color = Color.white;
                    glow_Color.enabled = true;
                    LeanTween.value(gameObject, (float val) =>
                 {
                     glow_Color.setAmountThres(val * 2.27f, 1 - (val * 0.5f));

                 }, 0, 1, 4f).setEaseInOutQuad().setOnComplete(() =>
                 {
                     canvas.blocksRaycasts = true;
                 });
                    break;

            }
            child.color = Color.clear;
        }
    }

}
