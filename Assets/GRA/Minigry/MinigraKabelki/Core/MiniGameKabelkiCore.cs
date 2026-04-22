using Unity.VisualScripting;
using UnityEngine;

public class MiniGameKabelkiCore : MonoBehaviour,IInteractable, IUnlockableMiniGame
{

    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;
    [SerializeField] private MiniGameKabelkiMenu _menu;
    [SerializeField] private MiniGameKabelkiRULE _zasady;
 // flipflop 
    private bool m_jestInterakcja=false;
    protected void SetJestInterakcja(bool isInteractja){m_jestInterakcja=isInteractja;}
    protected bool IsInteractja(){return m_jestInterakcja;}
    [SerializeField] private GameObject _gracz;
    public GameObject GetGracz(){return _gracz;}

    //audio
    [SerializeField] private AudioSource source;
   [SerializeField] private AudioClip openClip;
   [SerializeField] private AudioClip closeClip;

   [SerializeField] private string miniGameID;
   [SerializeField] private GameObject questMark;

   private bool isUnlocked = false;

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
        _gracz.GetComponentInChildren<cameraFollow>().CameraFollow(false);
        GetGracz().GetComponentInChildren<Latarka>().CanvasLightActive(true);
    }   
    protected void PlayersEnabes()
    {
           _gracz.GetComponentInChildren<cameraFollow>().CameraFollow(true);
         GetGracz().GetComponentInChildren<Latarka>().CanvasLightActive(false);
    _gracz.GetComponent<PlayerMovement>().CanMove(true);

 _gracz.GetComponent<PlayerRotation>().enabled=true;
    }


    public void Start()
    {
        _menu.GetMenuCanvas().SetActive(false);
        m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;

        if (EventManager.Instance != null)
        {
            EventManager.Instance.RegisterMiniGame(miniGameID, this);
            EventManager.Instance.RegisterMiniGameTarget(miniGameID, transform);
        }
        if (_zasady != null)
            _zasady.OnWin += FinishMiniGame;
    }
    private void OnDestroy()
    {
        if (_zasady != null)
            _zasady.OnWin -= FinishMiniGame;
    }
    public void Interact(GameObject gameObject,InteractorType interactor)
    {
        switch (interactor)
        {
            case InteractorType.Gracz:
            InteractPlayer();
            break;
            case InteractorType.Npc:
            InteractNpc(gameObject);
            break;
        }
    }
    public void InteractPlayer()
    {
        if (!isUnlocked)
            return;
        if (IsInteractja()){
           _zasady.StopALL();
            TurnOFFInteract();
          SetJestInterakcja(false);
        }
        else{
            Canvas.ForceUpdateCanvases();
             _zasady.ReBuild();
             TurnONInteract();
            SetJestInterakcja(true);
        }
    }
    public void InteractNpc(GameObject gameObject)
    {
        gameObject.GetComponent<SomsiadAI>().ZnajdzStatystyke("nuda").GetStat().addToCurrStat(100);
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

    public void UnlockMiniGame()
    {
        isUnlocked = true;
    }

    public void LockMiniGame()
    {
        isUnlocked = false;
        EventManager.Instance.LockMiniGame(miniGameID);
    }
    private void FinishMiniGame()
    {
        TurnOFFInteract();
        LockMiniGame();

        if (questMark != null)
            Destroy(questMark);
    }
}