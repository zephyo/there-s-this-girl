using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public virtual void Awake()
    {
        if (!instance)
        {
            instance = this as AudioManager;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        Application.targetFrameRate = 30;
    }
}
