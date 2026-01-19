using Unity.VisualScripting;
using UnityEngine;

public class PcCORE :Core, IInteractable
{

    
    //boxColider PC, ten od kolizji, na rodzicu 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;

    [SerializeField] private PcInventory _inventory;
    public override Inventory GetInventory()
    {
        return _inventory;
    }

    public void Start()
    {
        _inventory.GetMenu().GetMenuCanvas().SetActive(false);
        _inventory.GetMenu().SetForItemSlots();
        _inventory.GetMenu().ReloadInventory();
            m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;
      //  _pcInventory.GetPcMenu().GetInventory() = _pcInventory;

    }
    public void Interact()
    {
        if (IsInteractja()){
            TurnOFFInteract();
          SetJestInterakcja(false);
        }
        else{
             TurnONInteract();
            SetJestInterakcja(true);
        }
    }

    public void TurnONInteract(){
    Debug.Log("Otwieram PC");
    PlayersDisabes();
    //dźwięk otwierania PC
    PlayAudioOn();
    //grafika zapisanie spritea oraz zmiana na sprite otwartego PC

    GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;

    //uruchomienie menu PC
     _inventory.GetMenu().GetMenuCanvas().SetActive(true);
     _inventory.GetMenu().ReloadInventory();
    }
    public void TurnOFFInteract(){
    Debug.Log("Zamykam PC");
    PlayersEnabes();

    //dźwięk zamykania PC;
    PlayAudioOff();

    //wyłączenie menu PC
     _inventory.GetMenu().GetMenuCanvas().SetActive(false);
    
    //grafika
    GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    _inventory.GetMenu().UnFocusAll();
    _inventory.GetMenu().UnSelect();
    }
}
