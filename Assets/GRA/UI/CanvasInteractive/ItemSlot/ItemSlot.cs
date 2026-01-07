using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public abstract class ItemSlot : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    protected Przedmiot _przedmiot;
    public Przedmiot GetPrzedmiot(){return _przedmiot;}
    [SerializeField] protected Image _img;
    protected bool m_isUsed=false;
    protected bool m_isSelected=false;
    protected Menu _menu;
    public bool IsUsed(){return m_isUsed;}

    //kiedy gracz klika na dany produkt
    public bool IsSelected(){return m_isSelected;}
    public void SetMenu(Menu lodM){_menu = lodM;}
    public Image GetImage(){return _img;}
    //jak gracz klinknie na iknoe produktu co ma się zadziać
    public virtual void Focus(bool czyMaBycFocus)
    {
        m_isSelected=czyMaBycFocus;
        if (!czyMaBycFocus)
        {
            _img.sprite=_przedmiot.GetSpriteNormal();
        }
    }
    public abstract void Refresh();
    
    
public abstract void Clear();

public virtual void AddItem(Przedmiot produkt)
{
    m_isUsed = true;
    _przedmiot = produkt;

    _img.enabled = true;
    _img.sprite = _przedmiot.GetSpriteNormal();
}

    public abstract void OnPointerClick(PointerEventData eventData);

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
       _img.sprite=_przedmiot.GetSpriteHighLight();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
       _img.sprite=_przedmiot.GetSpriteNormal();
    }
}
