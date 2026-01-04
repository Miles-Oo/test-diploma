using UnityEngine.EventSystems;
public class ItemSlotBiblioteczka : ItemSlot
{

    //jak gracz klinknie na iknoe produktu co ma się zadziać
    public override void Focus(bool czyMaBycFocus)
    {
        m_isSelected=czyMaBycFocus;
        if (!czyMaBycFocus)
        {
            _img.sprite=_przedmiot.GetSpriteNormal();
        }
    }
    public override void Refresh(){}
public override void Clear()
{
    m_isUsed = false;
    m_isSelected = false;
    _przedmiot = null;

    _img.sprite = null;
    _img.enabled = false;
}


        ///    należy tutaj zrobić sprawdzenie czy książka ma zawartość(is readable)
        ///    jeżeli tak to niech odpali się CAŁY system książki (trzeba to zrobić heh...)
        ///    dodatkowo trzeba poprawić, że jak po hover klika się to sprite znowu jest zwykły, powinien nadal być highlight     ///
    public override void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.button== PointerEventData.InputButton.Left)
        {
            if(m_isUsed){
            _menu.UnFocusAll();
            _menu.ShowButton();
            m_isSelected=true;
            _menu.UpdateDesc(this);
            //_img.sprite=_przedmiot.GetSpriteNormal();
        }}
    }
}
