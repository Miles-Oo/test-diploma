
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjFollowMouse : MonoBehaviour,IDragHandler,IPointerDownHandler
{ 
    private Vector2 _distance;
  
    public void OnDrag(PointerEventData eventData)
    {
       Vector2 pointerPosition=Camera.main.ScreenToWorldPoint(eventData.position);
        Vector2 newobjpos=pointerPosition-_distance;
        
             transform.position=newobjpos;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       _distance=Camera.main.ScreenToWorldPoint(eventData.position)-transform.position;
    }
}
