using UnityEngine;

public class BiblioteczkaCORE :Core, IInteractable
{

    
    //boxColider przedmiotu, ten od kolizji, na rodzicu 
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
        foreach (Ksiazka ksiazka in _inventory.GetPrzedmioty())
        {
            ksiazka.LoadFileToString();
        }
            m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;
      //  _lodowkaInventory.GetLodowkaMenu().GetInventory() = _lodowkaInventory;

    }
    public void Interact()
    {
        print("interakcja");
        
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
    Debug.Log("Otwieram:"+this);
    PlayersDisabes();
    //dźwięk otwierania
   // PlayAudioOn();
    //grafika zapisanie spritea oraz zmiana na sprite otwartej

    GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;

    //uruchomienie menu
     _inventory.GetMenu().GetMenuCanvas().SetActive(true);
     _inventory.GetMenu().ReloadInventory();
    }
    public void TurnOFFInteract(){
    Debug.Log("Zamykam:"+this);
    PlayersEnabes();

    //dźwięk zamykania
   // PlayAudioOff();

    //wyłączenie menu
     _inventory.GetMenu().GetMenuCanvas().SetActive(false);
    
    //grafika
    GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    _inventory.GetMenu().UnFocusAll();
    _inventory.GetMenu().UnSelect();
    }
}
