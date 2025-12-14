using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lightsSwich : MonoBehaviour,IInteractable
{
   [SerializeField] private Light2D[] _lights;
   [SerializeField] private AudioSource source;
   [SerializeField] private AudioClip lightsOFF;
   [SerializeField] private AudioClip lightsON;
   private bool m_flipflop=false;
   void Start()
    {
        if (_lights == null){this.enabled=false;}
        else{
        for(int i =0;i<_lights.Length;i++){
        _lights[i].enabled=false;
       }
        }
    }
    
    public void Interact()
    {
        if (m_flipflop)LightsOFF();
        else LightsON();
        FlipFlop();
    }
    public void FlipFlop(){
        if (m_flipflop) m_flipflop=false;
        else if (!m_flipflop) m_flipflop=true;
    }
    public void LightsOFF()
    {
        source.PlayOneShot(lightsOFF);
        for(int i =0;i<_lights.Length;i++){
        _lights[i].enabled=false;
       }
    }

    public void LightsON()
    {
        source.PlayOneShot(lightsON);
        for(int i =0;i<_lights.Length;i++){
        _lights[i].enabled=true;
       }
    }

}
