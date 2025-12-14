using UnityEngine;
using UnityEngine.Rendering.Universal;

public class latarka : MonoBehaviour
{
   
   private bool m_isFlashlightOn=false;
   private Light2D _light;
    void Start()
    {
    _light=GetComponent<Light2D>();
    _light.enabled=false;
    }
    void Update()
    {
          if (Input.GetKeyDown(KeyCode.F))
        {
            if (m_isFlashlightOn)
            { 
                m_isFlashlightOn=false;
                _light.enabled=false;
            }
            else
            { 
                m_isFlashlightOn=true;
                _light.enabled=true;
            }
        }
    }
}
