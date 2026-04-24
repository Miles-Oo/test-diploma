using UnityEngine;

public class MiniGamePipesCore : MonoBehaviour, IInteractable, IUnlockableMiniGame
{
    [SerializeField] private GameObject _gracz;
    [SerializeField] private PipePuzzleUIMinigame controller;

    [Header("Sprites (optional)")]
    [SerializeField] private SpriteRenderer _targetRenderer;
    [SerializeField] private Sprite _sleepSprite;
    private Sprite _normalSprite;

    [Header("Menu")]
    [SerializeField] private MiniGamePipesMenu _menu;

    [Header("Audio")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;

    [SerializeField] private string miniGameID;
    [SerializeField] private float expReward = 50f;


    private bool _isOpen;

    private bool isUnlocked = false;

    private void Start()
    {
        if (_targetRenderer == null) _targetRenderer = GetComponentInParent<SpriteRenderer>();
        if (_targetRenderer != null) _normalSprite = _targetRenderer.sprite;

        if (_menu != null && _menu.GetMenuCanvas() != null)
            _menu.GetMenuCanvas().SetActive(false);
        if (EventManager.Instance != null)
        {
            EventManager.Instance.RegisterMiniGame(miniGameID, this);
            EventManager.Instance.RegisterMiniGameTarget(miniGameID, transform);
        }

        if (controller != null)
            controller.OnWin += FinishMiniGame;
    }

    public void Interact(GameObject gameObject,InteractorType interactor)
    {
        if (!isUnlocked)
            return;
        if (_isOpen) Close();
        else Open();
        _isOpen = !_isOpen;
    }

    private void Open()
    {
        DisablePlayer();
        if (source != null && openClip != null) source.PlayOneShot(openClip);

        if (_targetRenderer != null && _sleepSprite != null)
            _targetRenderer.sprite = _sleepSprite;

        _menu.StartGame();
    }

    private void Close()
    {
        _menu.StopGame();

        EnablePlayer();
        if (source != null && closeClip != null) source.PlayOneShot(closeClip);

        if (_targetRenderer != null)
            _targetRenderer.sprite = _normalSprite;
    }

    private void DisablePlayer()
    {
        if (_gracz == null) return;
        var move = _gracz.GetComponent<PlayerMovement>();
        if (move != null) move.CanMove(false);

        var rot = _gracz.GetComponent<PlayerRotation>();
        if (rot != null) rot.enabled = false;

        var lat = _gracz.GetComponentInChildren<Latarka>();
        if (lat != null) lat.CanvasLightActive(true);
    }

    private void EnablePlayer()
    {
        if (_gracz == null) return;
        var lat = _gracz.GetComponentInChildren<Latarka>();
        if (lat != null) lat.CanvasLightActive(false);

        var move = _gracz.GetComponent<PlayerMovement>();
        if (move != null) move.CanMove(true);

        var rot = _gracz.GetComponent<PlayerRotation>();
        if (rot != null) rot.enabled = true;
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
        Debug.Log("FINISH MINI GAME CALLED");

        if (controller != null)
            controller.OnWin -= FinishMiniGame;

        controller.StopRun();
        if (MiniGameRewardManager.Instance != null)
        {
            MiniGameRewardManager.Instance.GiveReward(miniGameID, expReward);
        } 

        Close();

        LockMiniGame();
    }
}
