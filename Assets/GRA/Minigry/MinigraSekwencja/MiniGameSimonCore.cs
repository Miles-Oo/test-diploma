using UnityEngine;

public class MiniGameSimonCore : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _gracz;
    [SerializeField] private MiniGameSimonMenu _menu;

    [Header("Audio (optional)")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;

    [Header("Sprites (optional)")]
    [SerializeField] private SpriteRenderer _targetRenderer;
    [SerializeField] private Sprite _sleepSprite;
    private Sprite _normalSprite;

    private bool _isOpen = false;

    private void Start()
    {
        if (_targetRenderer == null)
            _targetRenderer = GetComponentInParent<SpriteRenderer>();

        if (_targetRenderer != null)
            _normalSprite = _targetRenderer.sprite;

        if (_menu != null && _menu.GetMenuCanvas() != null)
            _menu.GetMenuCanvas().SetActive(false);
    }

    public void Interact(GameObject interactor, InteractorType interactorType)
    {
        // jeśli gracz nie podpięty w Inspectorze, bierzemy z parametru
        if (_gracz == null) _gracz = interactor;

        if (_isOpen) Close();
        else Open();

        _isOpen = !_isOpen;
    }

    private void Open()
    {
        if (_menu == null)
        {
            Debug.LogError("MiniGameSimonCore: _menu niepodpięte.");
            return;
        }

        DisablePlayer();

        if (source != null && openClip != null) source.PlayOneShot(openClip);
        if (_targetRenderer != null && _sleepSprite != null) _targetRenderer.sprite = _sleepSprite;

        _menu.StartGame();
    }

    private void Close()
    {
        if (_menu != null) _menu.StopGame();

        EnablePlayer();

        if (source != null && closeClip != null) source.PlayOneShot(closeClip);
        if (_targetRenderer != null) _targetRenderer.sprite = _normalSprite;
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
}