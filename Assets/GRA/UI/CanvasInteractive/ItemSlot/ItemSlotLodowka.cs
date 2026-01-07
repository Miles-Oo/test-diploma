using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class ItemSlotLodowka : ItemSlot
{
    [SerializeField] private TMP_Text _ilosc;

    //jak gracz klinknie na iknoe produktu co ma się zadziać
    public override void Focus(bool czyMaBycFocus)
    {
        m_isSelected=czyMaBycFocus;
        if (!czyMaBycFocus)
        {
            _img.sprite=_przedmiot.GetSpriteNormal();
            _ilosc.color=new Color(1,1,1);
        }
    }
    public override void Refresh()
    {
          _ilosc.text = "x" + _przedmiot.GetIloscWEQ();
    }
public override void Clear()
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

public override void AddItem(Przedmiot produkt)
{
    base.AddItem(produkt);
    _ilosc.enabled = true;
    _ilosc.text = "x" + _przedmiot.GetIloscWEQ();
}

    public override void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.button== PointerEventData.InputButton.Left)
        {
            if(m_isUsed){
            _menu.UnFocusAll();
            _menu.ShowButton();
            m_isSelected=true;
            _menu.UpdateDesc(this);
            _ilosc.color=new Color(0.2f,1f,0.35f);
            _img.sprite=_przedmiot.GetSpriteHighLight();
        }}
    }   
     public override void OnPointerExit(PointerEventData eventData)
    {
        if(!m_isSelected){
       _img.sprite=_przedmiot.GetSpriteNormal();
        }
    }     public override void OnPointerEnter(PointerEventData eventData)
    {
        if(!m_isSelected){
       _img.sprite=_przedmiot.GetSpriteHighLight();
        }
    }
}
