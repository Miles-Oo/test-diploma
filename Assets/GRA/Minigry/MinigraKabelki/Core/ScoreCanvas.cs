using UnityEngine;
using TMPro;
public class ScoreCanvas : MonoBehaviour
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
        _text.text="Runda: "+_rule.GetScore();
    }
}
