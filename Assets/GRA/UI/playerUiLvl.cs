using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerUiLvl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text1;
    [SerializeField] TextMeshProUGUI _text2;
    [SerializeField] private Image _fill;
    [SerializeField] leveling _leveling;

    private Color color;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        color=new Color(255,255,255);
        _text1.text="player lvl";
        _text2.text="curr exp/nextlevelcap";
        _leveling.OnExpChange += TextUpdate;
        TextUpdate();
    }
    void TextUpdate()
    {
        float precent = _leveling.getCurrentExp() / (float)_leveling.getNextLvlExpCap();
        _fill.fillAmount =precent;
        _text1.text="lvl: "+_leveling.getLvl();
        _text2.text=_leveling.getCurrentExp()+"/"+_leveling.getNextLvlExpCap();
    }
}
