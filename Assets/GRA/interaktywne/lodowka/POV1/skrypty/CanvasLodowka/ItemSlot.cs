using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
        [SerializeField] private TMP_Text _ilosc;
    private Przedmiot _przedmiot;
    public Przedmiot GetProdukt(){return _przedmiot;}
    [SerializeField] private Image _img;
    private bool m_isUsed=false;
    private bool m_isSelected=false;

    //ahhh tak ulany jeden plik ze wszystkim potem trzeba to przerobić....
    private LodowkaMenu _lodowkaMenu;
    public bool IsUsed(){return m_isUsed;}

    //kiedy gracz klika na dany produkt
    public bool IsSelected(){return m_isSelected;}
    public void SetLodowka(LodowkaMenu lodM)
    {
        _lodowkaMenu = lodM;
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
            _img.sprite=_przedmiot.GetSpriteNormal();
            _ilosc.color=new Color(1,1,1);
        }
    }
    public void Refresh()
    {
          _ilosc.text = "x" + _przedmiot.GetIloscWEQ();
    }
public void Clear()
{
    m_isUsed = false;
    m_isSelected = false;
    _przedmiot = null;

    _img.sprite = null;
    _img.enabled = false;

    _ilosc.text = "";
    _ilosc.color = Color.white;
    _ilosc.enabled = false;
}

public void AddItem(Przedmiot produkt)
{
    m_isUsed = true;
    _przedmiot = produkt;

    _img.enabled = true;
    _img.sprite = _przedmiot.GetSpriteNormal();

    _ilosc.enabled = true;
    _ilosc.text = "x" + _przedmiot.GetIloscWEQ();
}

    public void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.button== PointerEventData.InputButton.Left)
        {
            if(m_isUsed){
            _lodowkaMenu.UnFocusAll();
            _lodowkaMenu.ShowButton();
            m_isSelected=true;
            _lodowkaMenu.UpdateDesc(this);
            _ilosc.color=new Color(0.2f,1f,0.35f);
            _img.sprite=_przedmiot.GetSpriteHighLight();
        }}
    }
}
