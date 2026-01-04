using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class playerUiHungerBar : MonoBehaviour
{
    [SerializeField] private hunger _hunger;
    [SerializeField] private Image _fill;
    [SerializeField] private TextMeshProUGUI _text;

    private Color good;
    private Color bad;
    void Start(){
        _hunger.OnHungerChange += BarUpdate;
        BarUpdate();
        _text.text="pasek glodu";
        good=new Color(0,255,0);
        bad=new Color(255,0,0);
    }

    void BarUpdate(){
        float precent=_hunger.getCurrHunger() / (float)_hunger.getMaxHunger();
        _fill.fillAmount = precent;
        _text.text = "głód: "+_hunger.getCurrHunger()+"/"+_hunger.getMaxHunger();


        //jeżeli głód spadnie poniżej 20% pasek głodu zmieni kolor na czerwony w innym na zielony
        _fill.color = precent < 0.2 ? bad : good;

    }
}
