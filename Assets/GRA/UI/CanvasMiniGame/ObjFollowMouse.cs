using UnityEngine;
using UnityEngine.EventSystems;

public class ObjFollowMouse : MonoBehaviour,IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       transform.position=mousePosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
