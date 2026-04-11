using UnityEngine;
using TMPro;
public class playerUiTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] gamewordTimer _time;
    private System.TimeSpan t;
    void Start()
    {
        _time.OnTimeChange += TextUpdate;
        TextUpdate();
        _text.text="czas";
    }

    void TextUpdate()
    {
        t =System.TimeSpan.FromSeconds(_time.getCurrentSeconds());
        _text.text="czas: "+string.Format("{0:D2}:{1:D2}:{2:D2}",t.Hours,t.Minutes,t.Seconds);
    }
}
