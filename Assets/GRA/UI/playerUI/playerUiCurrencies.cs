using UnityEngine;
using TMPro;

public class playerUiCurrencies : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textZlowtowki;
    [SerializeField] TextMeshProUGUI _textKrypto;
    [SerializeField] wallet _wallet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textZlowtowki.text="zloty";        
        _textKrypto.text="krypto";
        _wallet.OnGetMoney += TextUpdate;
    }
    void TextUpdate()
    {
        _textZlowtowki.text="złotówki: "+_wallet.getZlotowki()+" zł";
        _textKrypto.text="krypto: "+_wallet.getKrypto()+" kr";
    }
}
