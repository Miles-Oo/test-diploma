using Unity.VisualScripting;
using UnityEngine;

public class MiniGameCore :Core, IInteractable
{

    
    //boxColider łóżka, ten od kolizji, na rodzicu 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;

    [SerializeField] private Menu _menu;

    public void Start()
    {
        _menu.GetMenuCanvas().SetActive(false);
        _menu.SetForItemSlots();
        _menu.ReloadInventory();
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
     _menu.GetMenuCanvas().SetActive(true);
     _menu.ReloadInventory();
    }
    public void TurnOFFInteract(){
    Debug.Log("Zamykam Lodówkę");
    PlayersEnabes();

    //dźwięk zamykania lodówki;
    PlayAudioOff();

    //wyłączenie menu lodówki
     _menu.GetMenuCanvas().SetActive(false);
    
    //grafika
    GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    _menu.UnFocusAll();
    _menu.UnSelect();
    }
}
