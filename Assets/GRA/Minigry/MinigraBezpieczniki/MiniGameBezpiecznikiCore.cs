using Unity.VisualScripting;
using UnityEngine;

public class MiniGameBezpiecznikiCore: MonoBehaviour,IInteractable
{

    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;

    [SerializeField] private MiniGameBezpiecznikiMenu _menu;
    [SerializeField] private MiniGameBezpiecznikiController _controller;

    private bool isUnlocked = false;
    [SerializeField] private GameObject questMark;






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
         GetGracz().GetComponentInChildren<Latarka>().CanvasLightActive(true);
    }   
    protected void PlayersEnabes()
    {
         GetGracz().GetComponentInChildren<Latarka>().CanvasLightActive(false);
    _gracz.GetComponent<PlayerMovement>().CanMove(true);

 _gracz.GetComponent<PlayerRotation>().enabled=true;
    }


    public void Start()
    {
        if (questMark != null)
            questMark.SetActive(false);
        _menu.GetMenuCanvas().SetActive(false);

            m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;
      //  _lodowkaInventory.GetLodowkaMenu().GetInventory() = _lodowkaInventory;

    }

    public void UnlockMiniGame()
    {
        isUnlocked = true;
        Debug.Log("MiniGra odblokowana!");

        if (questMark != null)
            questMark.SetActive(true);
    }

    public void LockMiniGame()
    {
        isUnlocked = false;
        Debug.Log("MiniGra zablokowana!");

        if (questMark != null)
            questMark.SetActive(false);
    }

    public void Interact()
    {
        if (!isUnlocked)
            {
                Debug.Log("MiniGra jeszcze zablokowana.");
                return;
            }

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

    // _controller.ResetGame(); //??


    PlayersDisabes();
    //dźwięk otwierania lodówki
    PlayAudioOn();
    //grafika zapisanie spritea oraz zmiana na sprite otwartej lodówki

    GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;

    //uruchomienie menu lodówki
     _menu.GetMenuCanvas().SetActive(true);

     _controller.StartGame();
     _controller.OnGameFinished += FinishMiniGame;

     _controller.OnExitRequested += ExitMiniGame;


    }
    public void TurnOFFInteract(){
    Debug.Log("Zamykam Lodówkę");

    _controller.OnExitRequested -= ExitMiniGame;
    _controller.OnGameFinished -= FinishMiniGame;

    _controller.ResetGame();

    PlayersEnabes();

    //dźwięk zamykania lodówki;
    PlayAudioOff();

    //wyłączenie menu lodówki
     _menu.GetMenuCanvas().SetActive(false);
    
    //grafika
    GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    _menu.UnSelect();
    }

    private void FinishMiniGame()
    {
        _controller.OnGameFinished -= FinishMiniGame;
        TurnOFFInteract();
        LockMiniGame();
    }
    
    private void ExitMiniGame()
    {
        _controller.OnExitRequested -= ExitMiniGame;
        _controller.OnGameFinished -= FinishMiniGame;
        TurnOFFInteract();
    }
}
