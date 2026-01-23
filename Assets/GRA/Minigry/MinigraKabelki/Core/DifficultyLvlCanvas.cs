using UnityEngine;
using TMPro;
public class DifficultyLvlCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] MiniGameKabelkiRULE _rule;

    private Color zielony=Color.green;
    private Color niebieski=Color.blue;
    private Color czerwony=Color.red;
    private Color fioletowy=Color.purple;

    private Color m_currColor=Color.white;
    void Start()
    {
        _rule.OnLicznikForCanvasChange += TextUpdate;
        TextUpdate();
    }

    void TextUpdate()
    {
        switch (_rule.GetDifficulty())
        {
            case MiniGameKabelkiRULE.Difficulty.Low:
            m_currColor=zielony;
            break;
            case MiniGameKabelkiRULE.Difficulty.Medium:
            m_currColor=niebieski;
            break;
            case MiniGameKabelkiRULE.Difficulty.High:
            m_currColor=czerwony;
            break;
            case MiniGameKabelkiRULE.Difficulty.Max:
            m_currColor=fioletowy;
            break;
            default:
             m_currColor=Color.white;
            break;
        }
        string hex = ColorUtility.ToHtmlStringRGB(m_currColor);
     _text.text = $"<color=white>poziom: <color=#{hex}>{_rule.GetDifficulty()}";
    }
}