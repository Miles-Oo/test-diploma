using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{    [SerializeField] private GameObject _menu;
    public GameObject GetMenuCanvas(){return _menu;}
    [SerializeField] private TMP_Text _opis;
    [SerializeField] private ItemSlot[] _itemSlot;    
    [SerializeField] private Button _buttonEat;

    private ItemSlot _selectedSlot;


    public void Start()
    {
        _opis.text="";
        _buttonEat.gameObject.SetActive(false);
        SetForItemSlots();
    }



    public void SetForItemSlots()
    {
        for(int i =0;i<_itemSlot.Length; i++){
            _itemSlot[i].SetLodowka(this);
        }
    }

    public void UpdateDesc(ItemSlot itemSlot)
    {
        _selectedSlot = itemSlot;
         _opis.text=_selectedSlot.GetPrzedmiot().GetText();
    }

    public void UnFocusAll()
    {
        for(int j=0;j< _itemSlot.Length; j++){
            if (_itemSlot[j].IsUsed()){
                _itemSlot[j].Focus(false);
            }
            }
    }

    public void UnSelect()
    {
        _selectedSlot=null;
        _opis.text="";
        _buttonEat.gameObject.SetActive(false);
    }
    public void ShowButton()
    {
        _buttonEat.gameObject.SetActive(true);
    }
public void ReloadInventory<T>(Inventory<T> inventory ) where T:Przedmiot
{
    // reset UI
    for (int i = 0; i < _itemSlot.Length; i++)
        _itemSlot[i].Clear();

    // ponowne ładowanie produktów
    for (int i = 0; i < inventory.GetPrzedmioty().Length; i++)
    {
        if (inventory.GetPrzedmioty()[i].GetIloscWEQ() <= 0)
            continue;

        for (int j = 0; j < _itemSlot.Length; j++)
        {
            if (!_itemSlot[j].IsUsed())
            {
                _itemSlot[j].AddItem(inventory.GetPrzedmioty()[i]);
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
    _opis.text = "";
    _buttonEat.gameObject.SetActive(false);
}

}
