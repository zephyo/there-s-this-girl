using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ScrollSnaps;

public class DisableOnScene : MonoBehaviour
{

    public List<int> disableScenes;

    private HashSet<int> disabledAlready = new HashSet<int>();

    public void checkDisable(int i)
    {
        if (disableScenes.Contains(i) && !disabledAlready.Contains(i))
        {
            GetComponent<DirectionalScrollSnap>().active = false;
            disabledAlready.Add(i);
        }
    }
}
