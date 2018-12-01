using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif
public class GameController : MonoBehaviour
{
    public CameraFilterPack_Blur_Movie movie;

    float scale;

    bool loaded = false;

    private void Start()
    {

#if UNITY_WEBGL
        Shader.EnableKeyword("UNITY_UI_CLIP_RECT");
#endif
        // PlayerPrefs.SetInt("chapter", 5);

        float aspect = (float)Screen.width / (float)Screen.height;
        // Debug.Log("widht " + Screen.width + "height" + Screen.height + "aspect" + aspect);
        if (aspect > 0.625f)
        {
            RectTransform child = (RectTransform)transform.GetChild(0);
            scale = 0.2018f;
            child.localScale = Vector3.one * 1.2018f;
            child.anchoredPosition = new Vector3(-384.05f, child.anchoredPosition.y, 0);
        }
        else
        {
            scale = 0;
        }
        loadHome();

    }


    void loadHome()
    {
        CanvasGroup cg = transform.GetChild(0).GetComponent<CanvasGroup>();
        movie.enabled = true;
        LeanTween.value(gameObject, (float val) =>
        {
            movie.Radius = 500 - (500 * val);
            cg.alpha = Mathf.Clamp(val * 2f, 0, 1);
        }, 0, 1, 1f).setEaseInQuart().setOnComplete(() =>
        {
            cg.blocksRaycasts = true;
            movie.enabled = false;
        });

        //set audio
        cg.transform.GetChild(0).Find("settings").GetComponent<Settings>().SetMixer();
    }

    public void Play(GameObject playTitle)
    {
        //enter chapter screen
        RectTransform home = (RectTransform)transform.GetChild(0);
        playTitle.gameObject.SetActive(false);
        Transform chapters = playTitle.transform.parent.Find("chapters");
        if (!loaded)
        {
            int chap = PlayerPrefs.GetInt("chapter", 1);
            if (chap < 5)
            {
                Transform content = chapters.Find("scroll").GetChild(0);
                for (int i = chap; i < 5; i++)
                {
                    content.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "???";
                }
            }
        }
        chapters.gameObject.SetActive(true);
        CanvasGroup CG = chapters.GetComponent<CanvasGroup>();
        LeanTween.value(gameObject, (float val) =>
        {
            home.localScale = Vector3.one * val;
            CG.alpha = val;
        }, 1 + scale, 1.33f + scale, 1f).setEaseInOutQuad().setOnComplete(() =>
            {
                CG.interactable = true;
                CG.blocksRaycasts = true;
            });
    }

    public void back(GameObject setActive)
    {
        RectTransform home = (RectTransform)transform.GetChild(0);
        Transform screen = home.GetChild(0);
        foreach (Transform child in screen)
        {
            if (child.gameObject == setActive)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        if (setActive.name == "title")
        {

            LeanTween.value(gameObject, (float val) =>
            {
                home.localScale = Vector3.one * val;
            }, 1.33f + scale, 1 + scale, 1f).setEaseInOutQuad();
        }
    }



    public void selectChapter(int chapter)
    {
        if (PlayerPrefs.GetInt("chapter", 1) < chapter)
        {
            return;
        }
        Transform Screen = transform.GetChild(0).GetChild(0);
        Screen.Find("chapters").gameObject.SetActive(false);
        TextMeshProUGUI chpTxt = Screen.Find("chp title").GetComponent<TextMeshProUGUI>();
        CanvasGroup TitleCG = chpTxt.GetComponent<CanvasGroup>();
        switch (chapter)
        {
            case 1:
                chpTxt.text = "<size=150%><color=#ff6363>I</color></size>\nbegin";
                break;
            case 2:
                chpTxt.text = "<size=150%><color=#ff6363>II</color></size>\nfall";
                break;
            case 3:
                chpTxt.text = "<size=150%><color=#ff6363>III</color></size>\nforget";
                break;
            case 4:
                chpTxt.text = "<size=150%><color=#ff6363>IV</color></size>\nheal";
                break;
            case 5:
                chpTxt.text = "<size=150%><color=#ff6363>V</color></size>\nimagine";
                break;
        }
        Button arrow = chpTxt.transform.Find("arrow").GetComponent<Button>();
        arrow.onClick.RemoveAllListeners();
        arrow.onClick.AddListener(() => { PlayChapter(chapter); });
        LeanTween.value(gameObject, (float val) =>
        {
            TitleCG.alpha = val;
        }, 0, 1, 0.25f).setEaseInOutQuad().setOnComplete(() =>
        {
            TitleCG.interactable = true;
            TitleCG.blocksRaycasts = true;
        });
        chpTxt.gameObject.SetActive(true);

    }

    public void fadeOutChpTitle(CanvasGroup ChpTitle)
    {

        LeanTween.value(gameObject, (float val) =>
        {

            ChpTitle.alpha = val;
        }, 1, 0, 0.25f).setEaseInOutQuad().setOnComplete(() =>
        {
            ChpTitle.interactable = false;
            ChpTitle.blocksRaycasts = false;
            ChpTitle.transform.parent.Find("chapters").gameObject.SetActive(true);
        });
    }



    public void PlayChapter(int chapter)
    {

        movie.enabled = true;
        AudioSource AS = EventSystem.current.GetComponent<AudioSource>();
        CanvasGroup cg = transform.GetChild(0).GetComponent<CanvasGroup>();
        LeanTween.value(gameObject, (float val) =>
        {
            cg.alpha = val * 1.5f;
            AS.volume = val;
            movie.Radius = 500 - (val * 500);

        }, 1, 0, 0.5f).setEaseOutQuad().setOnComplete(() =>
        {
            cg.blocksRaycasts = false;
            SceneManager.LoadScene(chapter.ToString());
        });
    }

    public void Tumblr()
    {
        openURL("https://zephyo.tumblr.com");
    }

    public void Facebook()
    {
        openURL("https://facebook.com/zephybite");
    }

    public void Twitter()
    {
        openURL("https://twitter.com/zephybite");
    }


    private void openURL(string url)
    {
#if UNITY_WEBGL
        openWindow(url);
#else
        Application.OpenURL(url);
#endif
    }
#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void openWindow(string url);
#endif


}
