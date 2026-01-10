using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightHandler : MonoBehaviour
{
    private float m_radius;
    public LayerMask layerMask;
    private Light2D _light2D_onMap;
    public Light2D GetLightOnMap(){return _light2D_onMap;}
    List<Collider2D> przedmioityInteractive=new List<Collider2D>();
    void Awake()
    {
        _light2D_onMap=GetComponent<Light2D>();
        m_radius=_light2D_onMap.pointLightOuterRadius;
//
         Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position,m_radius,layerMask);

        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<IInteractable>() != null)
            {
                
                if ( hit.transform.parent.GetComponentInChildren<CanvasLight2D>()!=null)
                {
                    przedmioityInteractive.Add(hit);
                    LightSet(hit);
                }
               
            }
        }
    }
    void Start()
    {
        
      
    }
  public void LightON()
    {
       
        LightOnMapON();
        LightCanvasON();
    }
   public void LightOFF()
    {
       
        LightOnMapOFF();
        LightCanvasOFF();
    }
    private void LightCanvasON()
    {
        foreach (Collider2D hit in przedmioityInteractive)
        {
            hit.transform.parent.GetComponentInChildren<CanvasLight2D>(true).GetLight2D().enabled=true;
        }
    }
    private void LightCanvasOFF()
    {
        foreach (Collider2D hit in przedmioityInteractive){
            hit.transform.parent.GetComponentInChildren<CanvasLight2D>(true).GetLight2D().enabled=false;
        }
    }

    private void LightOnMapON()
    {
        _light2D_onMap.enabled=true;
    }
    private void LightOnMapOFF()
    {
        _light2D_onMap.enabled=false;
    }
    private void LightSet(Collider2D hit)
    {   
        //odległość między światłem a obiektem do oświetlenia
        var distance= math.sqrt(math.pow(hit.transform.position.x-transform.position.x,2)+math.pow(  hit.transform.position.y-transform.position.y,2));
        //jeżeli pare świateł świeci na obiekt to wybierze najaśniejsze/najbiższe
        if (hit.transform.parent.GetComponentInChildren<CanvasLight2D>().GetLight2D().intensity<=_light2D_onMap.intensity/distance)
        {
        hit.transform.parent.GetComponentInChildren<CanvasLight2D>().GetLight2D().intensity=_light2D_onMap.intensity/distance;
        hit.transform.parent.GetComponentInChildren<CanvasLight2D>().GetLight2D().color=_light2D_onMap.color;
        }
       }
}
