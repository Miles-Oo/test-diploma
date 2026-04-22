using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameBalanceMenuDual : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private HoldToStabilizeMinigameDual _minigame;

    [Header("Handles (UI)")]
    [SerializeField] private RectTransform _handleA;
    [SerializeField] private RectTransform _handleB;
    [SerializeField] private float _yMin = -70f;
    [SerializeField] private float _yMax =  70f;

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

        // _minigame.OnWin = () =>
        // {
        //     Debug.Log("WIN dual stabilize!");
        //     // opcjonalnie: StopGame();
        // };
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

        // handle A
        if (_handleA != null)
        {
            float yA = Mathf.Lerp(_yMin, _yMax, _minigame.LevelA01);
            var p = _handleA.anchoredPosition;
            p.y = yA;
            _handleA.anchoredPosition = p;
        }

        // handle B
        if (_handleB != null)
        {
            float yB = Mathf.Lerp(_yMin, _yMax, _minigame.LevelB01);
            var p = _handleB.anchoredPosition;
            p.y = yB;
            _handleB.anchoredPosition = p;
        }

        // progress (countdown)
        if (_timerText != null)
            _timerText.text = _minigame.greenCountdown.ToString("0.0");
    }
}
