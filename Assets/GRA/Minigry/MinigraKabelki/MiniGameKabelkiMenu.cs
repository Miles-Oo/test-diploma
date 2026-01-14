using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MiniGameKabelkiMenu : MiniGameMenu{


 [SerializeField] protected GameObject _menu;
    public GameObject GetMenuCanvas(){return _menu;}

    protected ItemSlot _selectedSlot;


    public virtual void UpdateDesc(ItemSlot itemSlot)
    {
        _selectedSlot = itemSlot;

    }



    public virtual void UnSelect()
    {
        _selectedSlot=null;

    }
    public virtual void ShowButton()
    {

    }

public void UseSelected()
{
    if (_selectedSlot == null) return;

    var item = _selectedSlot.GetPrzedmiot();
    item.UsePrzedmiot();

    if (item.GetIloscWEQ() > 0)
    {
        _selectedSlot.Refresh();
        _selectedSlot.Focus(true);
        return;
    }

    _selectedSlot.Clear();
    _selectedSlot = null;
    OtherUsage();
}

public virtual void OtherUsage(){}



    [SerializeField] private TMP_Text _opis;
    [SerializeField] private Button _buttonEat;



}
