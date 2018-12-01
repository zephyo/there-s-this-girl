using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Chapter : MonoBehaviour
{

    public DirectionalScrollSnap scrollSnap;

    public int chapterNumber;

    public AudioClip song;
    public AudioClip quote;
    public float wait = 0.5f;
    public string quoteString;

    bool ending;

    public CanvasGroup canvas;

    bool quoteStarted = false;
    protected virtual void Start()
    {
        if (!quoteStarted)
        {
            quoteStarted = true;
            fadeInStart();
        }
        // scrollSnap.targetItemSelected.AddListener(onLastItem);
    }


    protected void fadeInStart()
    {
        AudioSource AS = EventSystem.current.GetComponent<AudioSource>();
        int time = AS.timeSamples;
        bool sameSong = AS.clip == song;
        AS.enabled = false;
        AS.clip = null;
        AS.volume = 1;
        AS.enabled = true;
        AS.loop = false;
        AS.PlayOneShot(quote, 1);
        Quote Q = GameObject.Instantiate(Resources.Load<GameObject>("quote")).GetComponent<Quote>();
        Q.Init(quoteString, quote.length, wait);
        StartCoroutine(fadeIn(AS, time, sameSong, Q));
    }

    protected virtual IEnumerator fadeIn(AudioSource AS, int time, bool sameSong, Quote Q)
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
        else
        {
            AS.timeSamples = 0;
        }

        AS.Play();
        canvas.blocksRaycasts = false;
        LeanTween.value(gameObject, (float val) =>
        {
            canvas.alpha = val;
            AS.volume = val;
        }, 0, 1, 0.7f).setEaseInQuad().setOnComplete(() =>
        {
            canvas.blocksRaycasts = true;
        });
    }

    private void fadeOut(Action onComp)
    {
        canvas.blocksRaycasts = false;
        LeanTween.value(gameObject, (float val) =>
        {
            canvas.alpha = val;
        }, 1, 0, 1.5f).setEaseOutQuad().setOnComplete(() =>
        {
            onComp();
        });
    }
    public virtual void onLastItem(int select)
    {
        // Debug.Log("target item "+select+" selected!");

        if (!ending && select >= scrollSnap.snapChildren.Count - 1)
        {
            ending = true;
            if (PlayerPrefs.GetInt("chapter", 1) < chapterNumber + 1)
            {
                PlayerPrefs.SetInt("chapter", chapterNumber + 1);
            }
            StartCoroutine(theEnd());
        }
    }

    protected IEnumerator theEnd()
    {
        yield return new WaitForSeconds(1.7f);
        fadeOut(loadStart);
    }

    private void loadStart()
    {
        SceneManager.LoadScene(0);
    }

    protected virtual void FadeIn(Action cb)
    {
        canvas.blocksRaycasts = false;
        LeanTween.value(gameObject, (float val) =>
        {
            canvas.alpha = val;
        }, 1, 0, 0.8f).setEaseInQuad().setOnComplete(() =>
        {
            if (cb != null)
                cb();
            LeanTween.value(gameObject, (float val) =>
            {
                canvas.alpha = val;
            }, 0, 1, 1f).setEaseOutQuad().setDelay(0.5f).setOnComplete(() =>
            {
                canvas.blocksRaycasts = true;
            });
        });

    }

}
