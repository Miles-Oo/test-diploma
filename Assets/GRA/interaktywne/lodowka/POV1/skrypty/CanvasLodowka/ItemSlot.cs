using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _ilosc;
    private Produkt _produkt;
    [SerializeField] private Image _img;
    private bool m_isUsed=false;

    public bool IsUsed(){return m_isUsed;}
    void Start()
    {
        
    }

    public void AddItem(Produkt produkt)
    {   
        m_isUsed=true;
        _produkt=produkt;
        _ilosc.text= "x"+produkt.getIleMaGracz();
        _ilosc.enabled=true;
        _img.sprite=_produkt.getSprite();
       
    }
    void Update()
    {
        
    }
}
