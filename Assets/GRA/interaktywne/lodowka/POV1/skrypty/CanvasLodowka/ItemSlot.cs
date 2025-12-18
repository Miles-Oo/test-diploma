using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
        [SerializeField] private TMP_Text _ilosc;
    private Produkt _produkt;
    [SerializeField] private Image _img;
    private bool m_isUsed=false;
    private bool m_isSelected=false;

    //ahhh tak ulany jeden plik ze wszystkim potem trzeba to przerobić....
    private LodowkaCORE _lodowkaCore;
    public bool IsUsed(){return m_isUsed;}

    //kiedy gracz klika na dany produkt
    public bool IsSelected(){return m_isSelected;}
    public void SetLodowka(LodowkaCORE lod)
    {
        _lodowkaCore = lod;
    }
    public Image GetImage()
    {
        return _img;
    }
    //jak gracz klinknie na iknoe produktu co ma się zadziać
    public void Focus(bool czyMaBycFocus)
    {
        m_isSelected=czyMaBycFocus;
        if (!czyMaBycFocus)
        {
            _img.sprite=_produkt.getSpriteNormal();
            _ilosc.color=new Color(1,1,1);
        }
    }
    public void AddItem(Produkt produkt)
    {   
        m_isUsed=true;
        _produkt=produkt;
        _ilosc.text= "x"+_produkt.getIleMaGracz();
        _ilosc.enabled=true;
        _img.sprite=_produkt.getSpriteNormal();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.button== PointerEventData.InputButton.Left)
        {
            if(m_isUsed){
            _lodowkaCore.UnFocusAll();
            m_isSelected=true;
            _ilosc.color=new Color(0.2f,1f,0.35f);
            _img.sprite=_produkt.getSpriteHighLight();
        }}
    }
}
