using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]


public class ButtonCore : MonoBehaviour
{
    void Awake()
    {
        var image = GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 0.1f;


    }
}
