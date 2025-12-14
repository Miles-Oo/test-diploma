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
   void Start()
    {
        if (_lights == null){this.enabled=false;}
        else{
        for(int i =0;i<_lights.Length;i++){
        _lights[i].enabled=false;
       }
        }
    }
    
    public void TurnOFFInteract()
    {
        source.PlayOneShot(lightsON);
        for(int i =0;i<_lights.Length;i++){
        _lights[i].enabled=false;
       }
    }

    public void TurnONInteract()
    {
        source.PlayOneShot(lightsOFF);
        for(int i =0;i<_lights.Length;i++){
        _lights[i].enabled=true;
       }
    }

}
