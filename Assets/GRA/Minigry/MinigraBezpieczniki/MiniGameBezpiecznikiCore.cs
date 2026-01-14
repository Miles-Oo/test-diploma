using Unity.VisualScripting;
using UnityEngine;

public class MiniGameBezpiecznikiCore: MonoBehaviour,IInteractable
{

    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;

    [SerializeField] private MiniGameBezpiecznikiMenu _menu;




 // flipflop 
    private bool m_jestInterakcja=false;
    protected void SetJestInterakcja(bool isInteractja)
    {
        m_jestInterakcja=isInteractja;
    }
    protected bool IsInteractja(){return m_jestInterakcja;}
    [SerializeField] private GameObject _gracz;
    public GameObject GetGracz(){return _gracz;}

    //audio
    [SerializeField] private AudioSource source;
   [SerializeField] private AudioClip openClip;
   [SerializeField] private AudioClip closeClip;


    protected void PlayAudioOn(){source.PlayOneShot(openClip);}
    protected void PlayAudioOff(){source.PlayOneShot(closeClip);}
    protected void PlayAudio(AudioClip audioClip){source.PlayOneShot(audioClip);}

    //nw jak to zrobić aby zwracał inventory, bo inventory może nie musi posidać i to różnego typu.
    public virtual Inventory GetInventory()
    {
        return null;
    }
    protected void PlayersDisabes(){
         _gracz.GetComponent<PlayerMovement>().CanMove(false); 
        //ZMIANA TUTAJ JAK JEDNAK LODÓWKA BEZ PRĄDU MA DZIAŁAĆ
        _gracz.GetComponent<PlayerRotation>().enabled=false;
         GetGracz().GetComponentInChildren<latarka>().CanvasLightActive(true);
    }   
    protected void PlayersEnabes()
    {
         GetGracz().GetComponentInChildren<latarka>().CanvasLightActive(false);
    _gracz.GetComponent<PlayerMovement>().CanMove(true);

 _gracz.GetComponent<PlayerRotation>().enabled=true;
    }


    public void Start()
    {
        _menu.GetMenuCanvas().SetActive(false);

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

    _menu.UnSelect();
    }
}
