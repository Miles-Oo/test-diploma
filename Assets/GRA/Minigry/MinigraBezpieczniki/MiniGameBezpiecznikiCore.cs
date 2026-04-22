using Unity.VisualScripting;
using UnityEngine;

public class MiniGameBezpiecznikiCore : MonoBehaviour, IInteractable, IUnlockableMiniGame
{
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;

    [SerializeField] private MiniGameBezpiecznikiMenu _menu;
    [SerializeField] private MiniGameBezpiecznikiController _controller;

    [SerializeField] private string miniGameID;
    [SerializeField] private GameObject questMark;

    private bool isUnlocked = false;

    // flipflop 
    private bool m_jestInterakcja = false;
    protected void SetJestInterakcja(bool isInteractja)
    {
        m_jestInterakcja = isInteractja;
    }
    protected bool IsInteractja() { return m_jestInterakcja; }

    [SerializeField] private GameObject _gracz;
    public GameObject GetGracz() { return _gracz; }

    // audio
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;

    protected void PlayAudioOn() { source.PlayOneShot(openClip); }
    protected void PlayAudioOff() { source.PlayOneShot(closeClip); }
    protected void PlayAudio(AudioClip audioClip) { source.PlayOneShot(audioClip); }

    public virtual Inventory GetInventory()
    {
        return null;
    }

    protected void PlayersDisabes()
    {
        _gracz.GetComponent<PlayerMovement>().CanMove(false);
        _gracz.GetComponent<PlayerRotation>().enabled = false;
        GetGracz().GetComponentInChildren<Latarka>().CanvasLightActive(true);
    }

    protected void PlayersEnabes()
    {
        GetGracz().GetComponentInChildren<Latarka>().CanvasLightActive(false);
        _gracz.GetComponent<PlayerMovement>().CanMove(true);
        _gracz.GetComponent<PlayerRotation>().enabled = true;
    }

    public void Start()
    {
        _menu.GetMenuCanvas().SetActive(false);

        m_normalSprite = GetComponentInParent<SpriteRenderer>().sprite;

        if (EventManager.Instance != null)
        {
            EventManager.Instance.RegisterMiniGame(miniGameID, this);
            EventManager.Instance.RegisterMiniGameTarget(miniGameID, transform);
        }
    }

    public void UnlockMiniGame()
    {
        isUnlocked = true;
    }

    public void LockMiniGame()
    {
        isUnlocked = false;

        if (EventManager.Instance != null)
        {
            EventManager.Instance.RemoveQuestMark(miniGameID);
        }
    }

    public void Interact(GameObject gameObject, InteractorType interactor)
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
        if (!isUnlocked)
        {
            return;
        }

        if (IsInteractja())
        {
            TurnOFFInteract();
            SetJestInterakcja(false);
        }
        else
        {
            TurnONInteract();
            SetJestInterakcja(true);
        }
    }

    public void InteractNpc()
    {
        // na przyszłość
    }

    public void TurnONInteract()
    {

        PlayersDisabes();
        PlayAudioOn();

        GetComponentInParent<SpriteRenderer>().sprite = m_sleepSprite;

        _menu.GetMenuCanvas().SetActive(true);

        _controller.StartGame();
        _controller.OnGameFinished += FinishMiniGame;
        _controller.OnExitRequested += ExitMiniGame;
    }

    public void TurnOFFInteract()
    {

        _controller.OnExitRequested -= ExitMiniGame;
        _controller.OnGameFinished -= FinishMiniGame;

        _controller.ResetGame();

        PlayersEnabes();
        PlayAudioOff();

        _menu.GetMenuCanvas().SetActive(false);

        GetComponentInParent<SpriteRenderer>().sprite = m_normalSprite;

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