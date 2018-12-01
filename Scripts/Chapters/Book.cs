using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.ScrollSnaps;

public class Book : MonoBehaviour
{

    public Image book;
    readonly Vector2 destination = new Vector2(-51, -24);

    bool finished = false;

    Camera main;

    private void Start()
    {
        main = Camera.main;
    }
    public void Drag(BaseEventData eventdata)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.root as RectTransform, Input.mousePosition, main, out pos);
        transform.position = transform.root.TransformPoint(pos);
        // book.rectTransform.anchoredPosition3D = Vector2.Lerp(book.rectTransform.anchoredPosition3D, main.WorldToScreenPoint(Input.mousePosition), Time.deltaTime);
    }


    public void Drop(BaseEventData eventdata)
    {
        if (!finished && Mathf.Abs(book.rectTransform.anchoredPosition.x - destination.x) < 40 &&
        Mathf.Abs(book.rectTransform.anchoredPosition.y - destination.y) < 60)
        {
            finished = true;
            book.rectTransform.anchoredPosition = destination;
            StartCoroutine(done());
        }
    }


    public void enableOn(int i)
    {
        if (i == 1)
        {
            gameObject.SetActive(true);
        }
    }
    IEnumerator done()
    {
        Destroy(gameObject.GetComponent<_2dxFX_Heat>());
        book.raycastTarget = false;
        yield return new WaitForSeconds(1.2f);
        DirectionalScrollSnap ds = transform.parent.parent.parent.GetComponent<DirectionalScrollSnap>();

        ds.enabled = true;

        yield return null;

        ds.ScrollToSnapPosition(1, 2, new Scroller.DecelerateInterpolator());
    }


}
