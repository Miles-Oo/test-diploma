using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LodowkaMenu : MonoBehaviour
{
    [SerializeField] private LodowkaInvetory _lodowkaInventory;

    [SerializeField] private GameObject _menu;
    public GameObject GetMenuCanvas(){return _menu;}
    [SerializeField] private TMP_Text _opis;
    [SerializeField] private ItemSlot[] _itemSlot;    
    [SerializeField] private Button _buttonEat;

    private ItemSlot _selectedSlot;


    public void Start()
    {
        _opis.text="";
        _buttonEat.gameObject.SetActive(false);
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
         _opis.text=_selectedSlot.GetProdukt().GetText();
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
public void ReloadInventory()
{
    // reset UI
    for (int i = 0; i < _itemSlot.Length; i++)
        _itemSlot[i].Clear();

    // ponowne ładowanie produktów
    for (int i = 0; i < _lodowkaInventory.GetProdukts().Length; i++)
    {
        if (_lodowkaInventory.GetProdukts()[i].GetIloscWEQ() <= 0)
            continue;

        for (int j = 0; j < _itemSlot.Length; j++)
        {
            if (!_itemSlot[j].IsUsed())
            {
                _itemSlot[j].AddItem(_lodowkaInventory.GetProdukts()[i]);
                break;
            }
        }
    }
}
public void EatProduct()
{

/*TUTAJ ZMIENIC ABY DZIALALO DLA ROZNYCH RZECZY*/
    if (_selectedSlot == null) return;

                    //new
    Przedmiot item = _selectedSlot.GetProdukt();
    if(item is Produkt p){
    if (p.GetIloscWEQ() <= 0) return;

    // logika gry
    p.SubIloscWEQ(1);
    _lodowkaInventory.GetLodowkaCORE().GetGracz().GetComponent<energy>().addEnergy(p.getEnergia());
    _lodowkaInventory.GetLodowkaCORE().GetGracz().GetComponent<hunger>().addHunger(p.getGlod());
    }
                    //new
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

    ReloadInventory();
}
}
