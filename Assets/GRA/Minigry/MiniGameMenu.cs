using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MiniGameMenu : Menu{
    [SerializeField] private TMP_Text _opis;
    [SerializeField] private Button _buttonEat;

    public override void Start()
    {
        base.Start();
        _opis.text="";
        _buttonEat.gameObject.SetActive(false);
    }

    public override void UpdateDesc(ItemSlot itemSlot)
    {
        _selectedSlot = itemSlot;
         _opis.text=_selectedSlot.GetPrzedmiot().GetText();
    }

    public override void UnSelect()
    {
        _selectedSlot=null;
        _opis.text="";
        _buttonEat.gameObject.SetActive(false);
    }
    public override void ShowButton()
    {
        _buttonEat.gameObject.SetActive(true);
    }


public override void OtherUsage(){
    _opis.text = "";
    _buttonEat.gameObject.SetActive(false);
    }

}
