using Unity.VisualScripting;
using UnityEngine;

public class LodowkaCORE :Core, IInteractable
{

    
    //boxColider łóżka, ten od kolizji, na rodzicu 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;

    [SerializeField] private LodowkaInvetory _lodowkaInventory;
    public override Inventory GetInventory()
    {
        return _lodowkaInventory;
    }

    public void Start()
    {
        _lodowkaInventory.GetLodowkaMenu().GetMenuCanvas().SetActive(false);
        _lodowkaInventory.GetLodowkaMenu().SetForItemSlots();
        _lodowkaInventory.GetLodowkaMenu().ReloadInventory();
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
    PlayAudioOn();
    //grafika zapisanie spritea oraz zmiana na sprite otwartej lodówki

    GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;

    //uruchomienie menu lodówki
     _lodowkaInventory.GetLodowkaMenu().GetMenuCanvas().SetActive(true);
     _lodowkaInventory.GetLodowkaMenu().ReloadInventory();
    }
    public void TurnOFFInteract(){
    Debug.Log("Zamykam Lodówkę");
    PlayersEnabes();

    //dźwięk zamykania lodówki;
    PlayAudioOff();

    //wyłączenie menu lodówki
     _lodowkaInventory.GetLodowkaMenu().GetMenuCanvas().SetActive(false);
    
    //grafika
    GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    _lodowkaInventory.GetLodowkaMenu().UnFocusAll();
    _lodowkaInventory.GetLodowkaMenu().UnSelect();
    }
}
