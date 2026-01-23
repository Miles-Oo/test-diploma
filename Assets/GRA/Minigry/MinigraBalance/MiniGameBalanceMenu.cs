using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MiniGameBalanceMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private HoldToStabilizeMinigame _minigame;

    [Header("Handle (UI)")]
    [SerializeField] private RectTransform _handleRect;
    [SerializeField] private float _yMin = -70f;
    [SerializeField] private float _yMax = 70f;

    [Header("UI (optional)")]
    [SerializeField] private TextMeshProUGUI _timerText; // Image Type=Filled

    public GameObject GetMenuCanvas() => _menuCanvas;

    private void Awake()
    {
        if (_menuCanvas != null) _menuCanvas.SetActive(false);
        if (_minigame != null) _minigame.enabled = false;
    }

    public void StartGame()
    {
        _menuCanvas.SetActive(true);

        _minigame.ResetRound();
        _minigame.enabled = true;

        // opcjonalnie: co zrobić po wygranej
        _minigame.OnWin = () =>
        {
            Debug.Log("WIN: stabilized!");
            // jeśli chcesz automatycznie zamknąć po wygranej:
            // StopGame();
        };
    }

    public void StopGame()
    {
        if (_minigame != null) _minigame.enabled = false;
        if (_menuCanvas != null) _menuCanvas.SetActive(false);
    }

    private void Update()
    {
        if (_menuCanvas == null || !_menuCanvas.activeSelf) return;
        if (_minigame == null) return;

        // 1) Move handle
        if (_handleRect != null)
        {
            float y = Mathf.Lerp(_yMin, _yMax, _minigame.Level01);
            var p = _handleRect.anchoredPosition;
            p.y = y;
            _handleRect.anchoredPosition = p;
        }

        // 2) Progress bar (optional)
        if (_timerText != null)
            _timerText.text = _minigame.greenCountdown.ToString("0.0");

    }
}
