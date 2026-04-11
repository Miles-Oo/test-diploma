using UnityEngine;

public class MiniGameBalanceCore : MonoBehaviour, IInteractable, IUnlockableMiniGame
{
    [Header("Collision (optional)")]
    [SerializeField] private BoxCollider2D _boxCollider2D;

    [Header("Sprites")]
    [SerializeField] private Sprite _sleepSprite;      // sprite "otwarty / używany"
    private Sprite _normalSprite;
    [SerializeField] private SpriteRenderer _targetRenderer; // jeśli null, weźmie z parenta

    [Header("Menu")]
    [SerializeField] private MiniGameBalanceMenuDual _menu;

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


    [Header("Player")]
    [SerializeField] private GameObject _gracz;
    public GameObject GetGracz() => _gracz;

    [Header("Audio")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;

    private void PlayAudioOn()
    {
        if (source != null && openClip != null) source.PlayOneShot(openClip);
    }
    private void PlayAudioOff()
    {
        if (source != null && closeClip != null) source.PlayOneShot(closeClip);
    }
    private void PlayAudio(AudioClip audioClip)
    {
        if (source != null && audioClip != null) source.PlayOneShot(audioClip);
    }

    private void PlayersDisabes()
    {
        if (_gracz == null) return;

        var move = _gracz.GetComponent<PlayerMovement>();
        if (move != null) move.CanMove(false);

        var rot = _gracz.GetComponent<PlayerRotation>();
        if (rot != null) rot.enabled = false;

        var lat = _gracz.GetComponentInChildren<Latarka>();
        if (lat != null) lat.CanvasLightActive(true);
    }

    private void PlayersEnabes()
    {
        if (_gracz == null) return;

        var lat = _gracz.GetComponentInChildren<Latarka>();
        if (lat != null) lat.CanvasLightActive(false);

        var move = _gracz.GetComponent<PlayerMovement>();
        if (move != null) move.CanMove(true);

        var rot = _gracz.GetComponent<PlayerRotation>();
        if (rot != null) rot.enabled = true;
    }

    private void Start()
    {
        // Renderer
        if (_targetRenderer == null)
            _targetRenderer = GetComponentInParent<SpriteRenderer>();

        if (_targetRenderer != null)
            _normalSprite = _targetRenderer.sprite;

        // Menu off na start
        if (_menu != null && _menu.GetMenuCanvas() != null)
            _menu.GetMenuCanvas().SetActive(false);
        
        if (EventManager.Instance != null)
        {
            EventManager.Instance.RegisterMiniGame(miniGameID, this);
            EventManager.Instance.RegisterMiniGameTarget(miniGameID, transform);
            EventManager.Instance.RegisterQuestMark(miniGameID, questMark);
        }
    }

    public void UnlockMiniGame()
    {
        isUnlocked = true;
        Debug.Log("Balance minigame odblokowana!");
    }

    public void LockMiniGame()
    {
        isUnlocked = false;
        Debug.Log("Balance minigame zablokowana!");
        EventManager.Instance.LockMiniGame(miniGameID);
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
        Debug.Log("Start minigry BALANCE");

        if (_menu == null)
        {
            Debug.LogError("MiniGameBalanceCore: _menu nie jest podpięte w Inspectorze!");
            return;
        }
        if (_gracz == null)
        {
            Debug.LogError("MiniGameBalanceCore: _gracz nie jest podpięty w Inspectorze!");
            return;
        }

        PlayersDisabes();
        PlayAudioOn();

        // sprite
        if (_targetRenderer != null && _sleepSprite != null)
            _targetRenderer.sprite = _sleepSprite;

        // menu + start minigry
        var canvas = _menu.GetMenuCanvas();
        if (canvas != null) canvas.SetActive(true);

        _menu.StartGame();
    }

    public void TurnOFFInteract()
    {
        Debug.Log("Koniec minigry BALANCE");

        // stop minigry + menu off
        if (_menu != null)
        {
            _menu.StopGame();
            var canvas = _menu.GetMenuCanvas();
            if (canvas != null) canvas.SetActive(false);
        }

        PlayersEnabes();
        PlayAudioOff();

        // sprite normalny
        if (_targetRenderer != null)
            _targetRenderer.sprite = _normalSprite;
    }
}
