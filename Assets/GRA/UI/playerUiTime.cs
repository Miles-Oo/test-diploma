using UnityEngine;
using TMPro;
public class playerUiTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] gamewordTimer _time;
    void Start()
    {
        _time.OnTimeChange += TextUpdate;
        TextUpdate();
        _text.text="czas";
    }

    void TextUpdate()
    {
        _text.text="czas: "+_time.getCurrentSeconds();
    }
}
