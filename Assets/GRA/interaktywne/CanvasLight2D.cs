using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CanvasLight2D : MonoBehaviour
{
   [SerializeField] private Light2D _light2d;
    public Light2D GetLight2D(){return _light2d;}
}
