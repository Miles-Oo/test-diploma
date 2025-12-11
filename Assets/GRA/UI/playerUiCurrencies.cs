using UnityEngine;
using TMPro;

public class playerUiCurrencies : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _lvlText;
    [SerializeField] inventory _inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lvlText.text="zloty krypto";
          _inventory.OnGetMoney += TextUpdate;
    }
    void TextUpdate()
    {
        _lvlText.text="złotówki: "+_inventory.getZlotowki()+"krypto: "+_inventory.getKrypto();
    }
}
