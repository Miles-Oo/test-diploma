using Unity.VisualScripting;
using UnityEngine;

public class LodowkaCORE : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _gracz;
    public GameObject GetGracz(){ return _gracz;}
    private bool m_jestInterakcja=false;
    //boxColider łóżka, ten od kolizji, na rodzicu 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;
   [SerializeField] private AudioSource source;
   [SerializeField] private AudioClip open;
   [SerializeField] private AudioClip close;



    [SerializeField] private LodowkaInvetory _lodowkaInventory;


    public void Start()
    {
        _lodowkaInventory.GetLodowkaMenu().GetMenuCanvas().SetActive(false);
        _lodowkaInventory.GetLodowkaMenu().SetForItemSlots();
        _lodowkaInventory.GetLodowkaMenu().ReloadInventory(_lodowkaInventory);
    }
    public void Interact()
    {
        if (m_jestInterakcja){
            TurnOFFInteract();
            m_jestInterakcja=false;
        }
        else{
             TurnONInteract();
             m_jestInterakcja=true;
        }
    }








    public void TurnONInteract()
    {
    Debug.Log("Otwieram Lodówkę");
    PlayersDisabes();

    //dźwięk otwierania lodówki
    source.PlayOneShot(open);

    //grafika zapisanie spritea oraz zmiana na sprite otwartej lodówki
    m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;
    GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;

    //uruchomienie menu lodówki
     _lodowkaInventory.GetLodowkaMenu().GetMenuCanvas().SetActive(true);
    }
    public void TurnOFFInteract()
    {
    Debug.Log("Zamykam Lodówkę");
    PlayersEnabes();

    //dźwięk zamykania lodówki;
    source.PlayOneShot(close);

    //wyłączenie menu lodówki
     _lodowkaInventory.GetLodowkaMenu().GetMenuCanvas().SetActive(false);
    
    //grafika
    GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    _lodowkaInventory.GetLodowkaMenu().UnFocusAll();
    _lodowkaInventory.GetLodowkaMenu().UnSelect();
    }

    private void PlayersDisabes(){
         _gracz.GetComponent<PlayerMovement>().CanMove(false); 
        //ZMIANA TUTAJ JAK JEDNAK LODÓWKA BEZ PRĄDU MA DZIAŁAĆ
      _gracz.GetComponentInChildren<latarka>().Lock();
        if (_gracz.GetComponentInChildren<latarka>().IsFlashlightOn())
        {
            _gracz.GetComponentInChildren<latarka>().turnOff();
        }
    }
    private void PlayersEnabes()
    {
    _gracz.GetComponent<PlayerMovement>().CanMove(true);
    _gracz.GetComponentInChildren<latarka>().Unlock();

    }
}
