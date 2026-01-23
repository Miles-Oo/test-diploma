using UnityEngine;
using UnityEngine.UI;

public class KabelekCore : MonoBehaviour
{
    [SerializeField] public GameObject _startowyKabel;
    [SerializeField] public GameObject _koncowyKabel;
    [SerializeField] private RuchomyKabel _ruchomyKabel;
    public RuchomyKabel GetRuchomyKabel(){return _ruchomyKabel;}
    [SerializeField] private GameObject _blob;
   private LineRenderer _line;
   private float m_maxPredkosc;
   public void SetMaxPredkosc(float maxPredkosc){m_maxPredkosc=maxPredkosc;}
   public static int InOrder=0;
   public LineRenderer GetLineRenderer()
    {
        return _line;
    }
    void Awake()
    {
    _line= _blob.GetComponent<LineRenderer>();
      }

    public void SetOnTopCanvas()
    {
       
        _startowyKabel.transform.SetAsLastSibling();
         _koncowyKabel.transform.SetAsLastSibling();
          _ruchomyKabel.transform.SetAsLastSibling();
           _blob.GetComponent<LineRenderer>().sortingOrder=100;
           Canvas.ForceUpdateCanvases();
    }
    public void SetOnNormalCanvas()
    {       InOrder++;
           _blob.GetComponent<LineRenderer>().sortingOrder=InOrder;
    }
void MapWorldToCanvasPosition()
{
RectTransform obj3RT = _startowyKabel.GetComponent<RectTransform>();
        Canvas canvas = obj3RT.GetComponentInParent<Canvas>();
        Camera cam = canvas.worldCamera;
        
        Vector2 screenPoint = cam.WorldToScreenPoint(obj3RT.position);
        RectTransform canvasRT = canvas.GetComponent<RectTransform>();
        Vector2 canvasPos;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRT, 
            screenPoint, 
            cam, 
            out canvasPos
        );
    _ruchomyKabel.SetStartPosition(canvasPos);
}
void MapWorldToCanvasPosition1()
{
RectTransform obj3RT = _koncowyKabel.GetComponent<RectTransform>();
        Canvas canvas = obj3RT.GetComponentInParent<Canvas>();
        Camera cam = canvas.worldCamera;
        
        Vector2 screenPoint = cam.WorldToScreenPoint(obj3RT.position);
        RectTransform canvasRT = canvas.GetComponent<RectTransform>();
        Vector2 canvasPos;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRT, 
            screenPoint, 
            cam, 
            out canvasPos
);

    _ruchomyKabel.SetEndPosition(canvasPos);
}


    void Start()
    {
        NewReset();
    }
    public void NewReset()
    {   InOrder=0;
        _ruchomyKabel.SetNewRuleDiff(m_maxPredkosc);
        _ruchomyKabel.SetConnected(false);
        MapWorldToCanvasPosition();
        MapWorldToCanvasPosition1();
      
       Vector2 d=_startowyKabel.GetComponent<RectTransform>().position;
       _line.SetPosition(0,d);
       _line.SetPosition(1,d);
       _ruchomyKabel.ResetPosition();
    }
}
