using UnityEngine;
using TMPro;
public class BiblioteczkaMenu : Menu{
    [SerializeField] private GameObject _ksiazka;
    [SerializeField] private TMP_Text _trescLewa;
    [SerializeField] private TMP_Text _trescPrawa;
    public override void Start()
    {
        base.Start();
       _ksiazka.SetActive(false);
    }

    public override void UpdateDesc(ItemSlot itemSlot)
    {
        _selectedSlot = itemSlot;
       Debug.Log(_selectedSlot.GetPrzedmiot().GetText());
       _ksiazka.SetActive(true);
       _trescLewa.text=_selectedSlot.GetPrzedmiot().GetText();
       _trescPrawa.text=_selectedSlot.GetPrzedmiot().GetText();
       _trescLewa.pageToDisplay=1;
       _trescPrawa.pageToDisplay=2;
    }

    public override void UnSelect()
    {
        _selectedSlot=null;
        _ksiazka.SetActive(false);
    }
    public override void ShowButton()
    {
      
    }


public override void OtherUsage(){
  
    }
    public void GoNextSide()
    {
        if(_trescLewa.pageToDisplay+2<=_trescLewa.textInfo.pageCount){
         _trescLewa.pageToDisplay=_trescLewa.pageToDisplay+=2;
       _trescPrawa.pageToDisplay=_trescPrawa.pageToDisplay+=2;
        }
    }
    public void GoPrewSide()
    {
        if (_trescLewa.pageToDisplay-2<0) return;
        
        _trescLewa.pageToDisplay=_trescLewa.pageToDisplay-=2;
        _trescPrawa.pageToDisplay=_trescPrawa.pageToDisplay-=2;
    
        }
       
}
