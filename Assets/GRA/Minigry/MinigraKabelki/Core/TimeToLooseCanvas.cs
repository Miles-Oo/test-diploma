using TMPro;
using UnityEngine;

public class TimeToLooseCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Timer _timer;
    private float m_criticalTime;
    void Start()
    {
        _timer.OnTimeChange += TextUpdate;
        TextUpdate();
        m_criticalTime=_timer.getTopTime()* 2/10;
    }

    void TextUpdate()
    {
        if(_timer.GetCurrTime()< m_criticalTime)
        {
            _text.text = $"pozostało: <color=red>{_timer.GetCurrTime():0.00}</color>";
        }
        else
        {
            _text.text="pozostało: "+$"{_timer.GetCurrTime():0.00}";
        }
        }
}
