using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetMixer()
    {
        if (PlayerPrefs.GetInt("music",1) == 0)
        {
            setMusic(false);
			transform.Find("music").GetComponent<Toggle>().isOn = false;
        }
        if (PlayerPrefs.GetInt("sfx",1) == 0)
        {
            setSfx(false);
			transform.Find("sfx").GetComponent<Toggle>().isOn = false;
        }
    }
    private void setMusic(bool on)
    {
        if (on)
        {
            mixer.SetFloat("MusicVol", 0);
        }
        else
        {
            mixer.SetFloat("MusicVol", -80);
        }
    }
    public void music(bool on)
    {
        PlayerPrefs.SetInt("music", on ? 1 : 0);
        setMusic(on);
    }
    private void setSfx(bool on)
    {
        if (on)
        {
            mixer.SetFloat("SfxVol", 0);
        }
        else
        {
            mixer.SetFloat("SfxVol", -80);
        }
    }
    public void sfx(bool on)
    {
        PlayerPrefs.SetInt("sfx", on ? 1 : 0);
        setSfx(on);
    }

	public void deleteData(){
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene(0);
	}
}
