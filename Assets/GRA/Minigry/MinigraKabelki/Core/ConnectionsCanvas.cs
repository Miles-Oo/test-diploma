using UnityEngine;
using TMPro;
public class ConnectionsCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] MiniGameKabelkiRULE _rule;
    void Start()
    {
        _rule.OnLicznikForCanvasChange += TextUpdate;
        TextUpdate();
    }

    void TextUpdate()
    {
        _text.text=_rule.GetCurrLicznik()+"/"+_rule.GetLicznik()+" połączono";
    }
}
