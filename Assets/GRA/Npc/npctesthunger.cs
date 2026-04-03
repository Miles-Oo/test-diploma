using UnityEngine;
using TMPro;
public class npctesthunger : MonoBehaviour
{
    [SerializeField] private Statystyka _stat;
    [SerializeField] private TextMeshProUGUI _text;

    void Start(){
        _stat.GetStat().OnStatChange += BarUpdate;
        BarUpdate();
        _text.text="pasek glodu";
    }

    void BarUpdate(){
        _text.text = _stat.GetStat().getStatName()+" : "+_stat.GetStat().getCurrStat()+"/"+_stat.GetStat().getMaxStat();
    }
}
