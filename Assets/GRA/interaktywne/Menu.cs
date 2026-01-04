using UnityEngine;
using UnityEngine.Video;
public abstract class Menu : MonoBehaviour
{    [SerializeField] protected GameObject _menu;
    public GameObject GetMenuCanvas(){return _menu;}
    [SerializeField] protected ItemSlot[] _itemSlot;    
    [SerializeField] protected Inventory _inventory;
    protected ItemSlot _selectedSlot;


    public virtual void Start()
    {
        SetForItemSlots();
    }



    public void SetForItemSlots()
    {
        for(int i =0;i<_itemSlot.Length; i++){
            _itemSlot[i].SetMenu(this);
        }
    }

    public virtual void UpdateDesc(ItemSlot itemSlot)
    {
        _selectedSlot = itemSlot;

    }

    public void UnFocusAll()
    {
        for(int j=0;j< _itemSlot.Length; j++){
            if (_itemSlot[j].IsUsed()){
                _itemSlot[j].Focus(false);
            }
            }
    }

    public virtual void UnSelect()
    {
        _selectedSlot=null;

    }
    public virtual void ShowButton()
    {

    }
public void ReloadInventory()
{
    // reset UI
    for (int i = 0; i < _itemSlot.Length; i++)
        _itemSlot[i].Clear();

    // ponowne ładowanie produktów
    for (int i = 0; i < _inventory.GetPrzedmioty().Count; i++)
    {
        if (_inventory.GetPrzedmioty()[i].GetIloscWEQ() <= 0)
            continue;

        for (int j = 0; j < _itemSlot.Length; j++)
        {
            if (!_itemSlot[j].IsUsed())
            {
                _itemSlot[j].AddItem(_inventory.GetPrzedmioty()[i]);
                break;
            }
        }
    }
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
    ReloadInventory();
}

public virtual void OtherUsage(){}
}
