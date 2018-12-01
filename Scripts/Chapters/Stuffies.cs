using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stuffies : MonoBehaviour
{
    public Sprite cats, bears, lions;

    public void cat()
    {
        GetComponent<Image>().sprite = cats;
    }
    public void bear()
    {
        GetComponent<Image>().sprite = bears;
    }
    public void lion()
    {
        GetComponent<Image>().sprite = lions;
    }

}
