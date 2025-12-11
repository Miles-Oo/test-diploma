using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class playerUiEnergyBar : MonoBehaviour
{
    [SerializeField] private energy _energy;
    [SerializeField] private Image _fill;
    [SerializeField] private TextMeshProUGUI _text;
    private Color good;
    private Color bad;

    void Start(){
        _energy.OnEnergyChange += BarUpdate;
        BarUpdate();
        _text.text="pasek energii";
        good=new Color(255,255,0);
        bad=new Color(255,0,0);
    }

    void BarUpdate(){
        float precent = _energy.getCurrEnergy() / (float)_energy.getMaxEnergy();
        _fill.fillAmount =precent;
        _text.text = "energia: "+_energy.getCurrEnergy()+"/"+_energy.getMaxEnergy();
           
        //jeżeli energia spadnie poniżej 20% pasek energii zmieni kolor na czerwony w innym przypadku na żółty
        _fill.color = precent < 0.2 ? bad : good;
    }
}
