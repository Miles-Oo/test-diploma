using UnityEngine;
using TMPro;

public class playerUiLvl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _lvlText;
    [SerializeField] leveling _leveling;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lvlText.text="player lvl";
          _leveling.OnExpChange += TextUpdate;
    }
    void TextUpdate()
    {
        _lvlText.text="poziom: "+_leveling.getLvl()+" "+_leveling.getCurrentExp()+"/"+_leveling.getNextLvlExpCap();
    }
}
