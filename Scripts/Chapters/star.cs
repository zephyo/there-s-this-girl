using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{

    public RectTransform RectTransform;
    public Animator animator;

    public HashSet<star> connectedTo = new HashSet<star>();

    private void Start()
    {
        RectTransform = (RectTransform)transform;
    }

    public void setConnected(star s)
    {
        connectedTo.Add(s);
        animator.SetTrigger("Connected");
    }
    public void setNormal()
    {
        if (connectedTo.Count > 0)
        {
            animator.SetTrigger("Connected");
        }
        else { animator.SetTrigger("Normal"); }
    }
}
