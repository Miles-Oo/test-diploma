using System;
using System.Collections;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RuchomyKabel: MonoBehaviour,IDragHandler,IPointerDownHandler,IEndDragHandler,IBeginDragHandler   
{ 
    public event Action OnlicznikChange;

    private Vector2 m_distance;
    private Vector2 m_startPosition;
    private Vector2 m_endPosition;
    private bool m_connected;
    public void SetConnected(bool con){m_connected=con;}
    public bool GetConnected(){return m_connected;}
    [SerializeField] private KabelekCore _kabelekCore;

    public Vector2 GetStartPosition(){return m_startPosition;}
    public void SetStartPosition(Vector2 positon){m_startPosition=positon;}
    public void SetEndPosition(Vector2 position){m_endPosition=position;}
    private RectTransform _rectTransform;
    public RectTransform GetRect(){return _rectTransform;}
    private float m_gap=200;
    public void SetNewRuleDiff(float maxPredkosc)
    {
       m_gap=maxPredkosc;
    }

    public void Awake()
    {
        _rectTransform=GetComponent<RectTransform>();
        m_connected=false;
    }
    bool jebaclysego=false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        jebaclysego=false;
        ciagnie=true;
        tick=StartCoroutine(Tick());
        _kabelekCore.ForceBorkeLine();
        _kabelekCore.SetOnTopCanvas();

    }

    public void OnDrag(PointerEventData eventData)
    {
        if(m_connected){return;}
        if(jebaclysego){return;};
       Vector2 pointerPosition=Camera.main.ScreenToWorldPoint(eventData.position);
        Vector2 newobjpos=pointerPosition-m_distance;
        newp=newobjpos;
       
   
        transform.position=newobjpos;
        _kabelekCore.GetLineRenderer().SetPosition(1,_rectTransform.position);
    }

private Coroutine tick;
private bool ciagnie=false;
Vector2 oldp;
Vector2 newp;
  IEnumerator Tick(){
        while(ciagnie){
            print("w trakcie");
            oldp = transform.position;
            yield return new WaitForSeconds(0.05f);
           print("po");
            ToFast(oldp,newp);
        }
    
    
    }
    private bool ToFast(Vector2 oldPos,Vector2 newPos)
    {
       
        if (Vector2.Distance(oldPos,newPos)>m_gap)
        {
           jebaclysego=true;
            ResetPosition();
            return true;
        }
        else
        {
            
            return false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       m_distance=Camera.main.ScreenToWorldPoint(eventData.position)-transform.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        ciagnie=false;
        
        StopCoroutine(tick);
            Vector2 ten =_rectTransform.anchoredPosition;
            Vector2 center = m_endPosition;
            float halfWidth  = 100/2;
            float halfHeight = 150/2; 
            //sprawdza kolizje dookoła konca kabla(snapowanie)
        if (ten.x >= center.x - halfWidth &&
            ten.x <= center.x + halfWidth &&
            ten.y >= center.y - halfHeight &&
            ten.y <= center.y + halfHeight){
                m_connected=true;
                
                _rectTransform.anchoredPosition=m_endPosition;
                _kabelekCore.GetLineRenderer().SetPosition(1,_rectTransform.position);
_kabelekCore.SetOnNormalCanvas();
            if (m_connected)
            {
            OnlicznikChange?.Invoke();
            }
        }
        else{ResetPosition();}
    }
    public void ResetPosition()
    {
        _rectTransform.anchoredPosition=m_startPosition;
        _kabelekCore.GetLineRenderer().SetPosition(1,_rectTransform.position);
    }
}
