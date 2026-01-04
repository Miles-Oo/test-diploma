using Unity.VisualScripting;
using UnityEngine;

public class BiblioteczkaCORE :Core, IInteractable
{

    
    //boxColider łóżka, ten od kolizji, na rodzicu 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;

    [SerializeField] private BiblioteczkaInvetory _inventory;
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
      //  _lodowkaInventory.GetLodowkaMenu().GetInventory() = _lodowkaInventory;

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
    Debug.Log("Otwieram Lodówkę");
    PlayersDisabes();
    //dźwięk otwierania lodówki
   // PlayAudioOn();
    //grafika zapisanie spritea oraz zmiana na sprite otwartej lodówki

    GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;

    //uruchomienie menu lodówki
     _inventory.GetMenu().GetMenuCanvas().SetActive(true);
     _inventory.GetMenu().ReloadInventory();
    }
    public void TurnOFFInteract(){
    Debug.Log("Zamykam Lodówkę");
    PlayersEnabes();

    //dźwięk zamykania lodówki;
   // PlayAudioOff();

    //wyłączenie menu lodówki
     _inventory.GetMenu().GetMenuCanvas().SetActive(false);
    
    //grafika
    GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    _inventory.GetMenu().UnFocusAll();
    _inventory.GetMenu().UnSelect();
    }
}
