using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;

public class LodowkaMenu : MonoBehaviour
{
    [SerializeField] private LodowkaInvetory _lodowkaInventory;

    [SerializeField] private GameObject _menu;
    public GameObject GetMenuCanvas(){return _menu;}
    [SerializeField] private TMP_Text _opis;
    [SerializeField] private Button _buttonEat;
    [SerializeField] private ItemSlot[] _itemSlot;
    private ItemSlot _selectedSlot;

    public void SetForItemSlots()
    {
        for(int i =0;i<_itemSlot.Length; i++){
            _itemSlot[i].SetLodowka(this);
        }
    }

    public void UpdateDesc(ItemSlot itemSlot)
    {
        _selectedSlot = itemSlot;
        
        String na= "Nazwa: "+itemSlot.GetProdukt().getNazwa()+
                    "\nOpis: "+itemSlot.GetProdukt().getOpis()+
                    "\nEnergia: "+itemSlot.GetProdukt().getEnergia()+
                    "\nGłód: "+itemSlot.GetProdukt().getGlod();
                _opis.text=na;
    }

    public void UnFocusAll()
    {
        for(int j=0;j< _itemSlot.Length; j++){
            if (_itemSlot[j].IsUsed()){
                _itemSlot[j].Focus(false);
            }
            }
    }


public void ReloadInventory()
{
    // reset UI
    for (int i = 0; i < _itemSlot.Length; i++)
        _itemSlot[i].Clear();

    // ponowne ładowanie produktów
    for (int i = 0; i < _lodowkaInventory.GetProdukts().Length; i++)
    {
        if (_lodowkaInventory.GetProdukts()[i].getIleMaGracz() <= 0)
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
    if (_selectedSlot == null) return;

    Produkt p = _selectedSlot.GetProdukt();
    if (p.getIleMaGracz() <= 0) return;

    // logika gry
    p.subIleMaGracz(1);
   _lodowkaInventory.GetLodowkaCORE().GetGracz().GetComponent<energy>().addEnergy(p.getEnergia());
    _lodowkaInventory.GetLodowkaCORE().GetGracz().GetComponent<hunger>().addHunger(p.getGlod());

    // aktualizacja UI
    _selectedSlot.Refresh();

    // jeśli skończył się produkt
    if (p.getIleMaGracz() <= 0)
    {
        _selectedSlot.Clear();
        _selectedSlot = null;
        _opis.text = "";
    }
    ReloadInventory();
}






}
