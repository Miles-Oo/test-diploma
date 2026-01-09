using UnityEngine;
using UnityEngine.Rendering.Universal;

public class latarka : MonoBehaviour
{
   
   private bool m_isFlashlightOn=false;
   private Light2D _light;
   private Light2D _lightInCanvas;
   [SerializeField] private GameObject _lightCanvas;
   public void CanvasLightActive(bool wylwl){_lightCanvas.SetActive(wylwl);}

   private SpriteRenderer _spriteRenderer;
   private bool m_lock;

    [SerializeField] private AudioSource source;
   [SerializeField] private AudioClip lightOFF;
   [SerializeField] private AudioClip lightON;

   public bool IsFlashlightOn()
    {
        return m_isFlashlightOn;
    }
    void Start()
    {
    _light=GetComponent<Light2D>();
    _lightInCanvas=_lightCanvas.GetComponent<Light2D>();
    _spriteRenderer=GetComponent<SpriteRenderer>();
    _light.enabled=false;
    _lightCanvas.SetActive(false);
    _spriteRenderer.enabled=false;

    m_lock=false;
    }
    void Update()
    {
          if (Input.GetKeyDown(KeyCode.F)){FlipFlop();}
    }

    public void Unlock(){m_lock=false;}
    public void Lock(){m_lock=true;}
    public void FlipFlop()
    {
        if (!m_lock&&!m_isFlashlightOn){turnOn();}
        else if(!m_lock){turnOff();}
    }
    public void turnOn()
    {
        source.PlayOneShot(lightON);
        _light.enabled=true;
        _lightInCanvas.enabled=true;
        _spriteRenderer.enabled=true;
        m_isFlashlightOn=true;
    }
    public void turnOff()
    {
        source.PlayOneShot(lightOFF);
         _light.enabled=false;
         _lightInCanvas.enabled=false;
        _spriteRenderer.enabled=false;
        m_isFlashlightOn=false;
    }
}
