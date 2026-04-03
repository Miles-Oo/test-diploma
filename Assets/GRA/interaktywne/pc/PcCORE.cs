using Unity.VisualScripting;
using UnityEngine;

public class PcCORE :Core, IInteractable
{

    
    //boxColider PC, ten od kolizji, na rodzicu 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;

    [SerializeField] private PcMenu _pcMenu;

    public void Start()
    {
        if (_pcMenu != null)
        {
            var menuCanvas = _pcMenu.GetMenuCanvas();
            if (menuCanvas != null)
                menuCanvas.SetActive(false);
        }
        m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;
    }
    public void Interact(GameObject gameObject,InteractorType interactor)
    {
        switch (interactor)
        {
            case InteractorType.Gracz:
            InteractPlayer();
            break;
            case InteractorType.Npc:
            InteractNpc();
            break;
        }
    }
    public void InteractPlayer()
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
    public void InteractNpc()
    {
        
    }

    public void TurnONInteract(){
    Debug.Log("Otwieram PC");
    PlayersDisabes();
    //dźwięk otwierania PC
    PlayAudioOn();
    //grafika zapisanie spritea oraz zmiana na sprite otwartego PC

    GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;

    //uruchomienie menu PC
    if (_pcMenu != null)
    {
        var menuCanvas = _pcMenu.GetMenuCanvas();
        if (menuCanvas != null)
            menuCanvas.SetActive(true);
        _pcMenu.ActivateTerminal();
    }
    }
    public void TurnOFFInteract(){
    Debug.Log("Zamykam PC");
    PlayersEnabes();

    //dźwięk zamykania PC;
    PlayAudioOff();

    //wyłączenie menu PC
    if (_pcMenu != null)
    {
        _pcMenu.DeactivateTerminal();
        var menuCanvas = _pcMenu.GetMenuCanvas();
        if (menuCanvas != null)
            menuCanvas.SetActive(false);
    }
    
    //grafika
    GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;
    }
}