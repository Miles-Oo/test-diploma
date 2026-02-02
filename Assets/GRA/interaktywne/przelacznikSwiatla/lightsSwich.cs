using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class lightsSwich : MonoBehaviour,IInteractable
{
   [SerializeField] private LightHandler[] _lights;
   private AudioSource _source;
   [SerializeField] private AudioClip lightsOFF;
   [SerializeField] private AudioClip lightsON;
   private bool m_flipflop=false;
  
  void Awake()
    {
        _source=GetComponent<AudioSource>();
    }
  
   void Start()
    {
        if (_lights == null){this.enabled=false;}
        else{
        for(int i =0;i<_lights.Length;i++){
        _lights[i].LightOFF();
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
        _source.PlayOneShot(lightsOFF);
        for(int i =0;i<_lights.Length;i++){
        _lights[i].LightOFF();
       }
    }

    public void LightsON()
    {
        _source.PlayOneShot(lightsON);
        for(int i =0;i<_lights.Length;i++){
        _lights[i].LightON();
       }
    }

}
