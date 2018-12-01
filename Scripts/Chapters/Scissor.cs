using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;

public class Scissor : MonoBehaviour
{

    public Image scisFront, scisBack;
    public Image pola1, pola2;

    public Image bg;

    public Sprite postcard;

    float startY;
    readonly Vector2 destination = new Vector2(36.2f, -200f);

    readonly Vector3 leftTarget = new Vector3(0, 0, -2.8f);
    readonly Vector3 rightTarget = new Vector3(0, 0, 9.8f);

    bool finished = false;

    Camera main;

    readonly Color black = new Color(0, 0, 0, 0.3f);
    private void Start()
    {
        main = Camera.main;
        startY = scisFront.rectTransform.anchoredPosition.y;
    }
    public void Drag(BaseEventData eventdata)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root as RectTransform, Input.mousePosition, main, out pos);
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.root.TransformPoint(pos).y), Time.deltaTime);
        scisBack.transform.position = new Vector2(scisBack.transform.position.x, transform.position.y);
        //-2.813
        //9.828
        float t = (scisFront.rectTransform.anchoredPosition.y - startY) / (destination.y - startY);
        pola1.transform.eulerAngles = Vector3.Lerp(Vector3.zero, leftTarget, t);
        pola2.transform.eulerAngles = Vector3.Lerp(Vector3.zero, rightTarget, t);
        bg.color = Color.Lerp(Color.white, black, t);
    }


    public void BeginDrag(BaseEventData eventdata)
    {
        scisFront.GetComponent<Animator>().enabled = true;
        scisBack.GetComponent<Animator>().enabled = true;
    }


    public void Drop(BaseEventData eventdata)
    {
        if (finished) return;
        scisFront.GetComponent<Animator>().enabled = false;
        scisBack.GetComponent<Animator>().enabled = false;
        RectTransform rt = (RectTransform)transform;
        if (rt.anchoredPosition.y > destination.y)
        {
            finished = true;
            // rt.anchoredPosition = destination;
            StartCoroutine(done());
        }
    }


    IEnumerator done()
    {
        Destroy(gameObject.GetComponent<_2dxFX_Heat>());
        scisFront.raycastTarget = false;
        yield return new WaitForSeconds(0.3f);
        DirectionalScrollSnap ds = transform.parent.parent.parent.GetComponent<DirectionalScrollSnap>();
        ds.active= true;
        ds.forceStartScroll();

    }

}
