using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeLastImage : MonoBehaviour
{

    public Image last;
    int lastIndex;

    private void Start()
    {
        lastIndex = last.transform.GetSiblingIndex();
    }

    public void DisableLast(int i)
    {
        if (i == lastIndex)
        {
            last.gameObject.SetActive(false);
        }
    }
}
