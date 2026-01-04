using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BiblioteczkaMenu : Menu{
    public override void Start()
    {
        base.Start();
       
    }

    public override void UpdateDesc(ItemSlot itemSlot)
    {
        _selectedSlot = itemSlot;
       
    }

    public override void UnSelect()
    {
        _selectedSlot=null;
       
       
    }
    public override void ShowButton()
    {
      
    }


public override void OtherUsage(){
  
    }

}
