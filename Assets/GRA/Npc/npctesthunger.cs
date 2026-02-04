using UnityEngine;
using TMPro;
public class npctesthunger : MonoBehaviour
{
    [SerializeField] private Statystyka _stat;
    [SerializeField] private TextMeshProUGUI _text;

    void Start(){
        _stat.structStat.OnStatChange += BarUpdate;
        BarUpdate();
        _text.text="pasek glodu";
    }

    void BarUpdate(){
        _text.text = _stat.structStat.getStatName()+" : "+_stat.structStat.getCurrStat()+"/"+_stat.structStat.getMaxStat();
    }
}
