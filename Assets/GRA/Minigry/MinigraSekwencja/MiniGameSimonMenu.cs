using UnityEngine;

public class MiniGameSimonMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas; // np. Panel w Canvasie
    [SerializeField] private SimonMinigameLogic _minigame;

    public GameObject GetMenuCanvas() => _menuCanvas;

    private void Awake()
    {
        if (_menuCanvas != null) _menuCanvas.SetActive(false);
        if (_minigame != null) _minigame.enabled = false;
    }

    public void StartGame()
    {
        if (_menuCanvas != null) _menuCanvas.SetActive(true);

        if (_minigame == null)
        {
            Debug.LogError("MiniGameSimonMenu: _minigame NULL.");
            return;
        }

        _minigame.OnWin = () =>
        {
            Debug.Log("SIMON WIN -> zamykam minigrę");
            StopGame();
        };

        _minigame.OnLose = () =>
        {
            Debug.Log("SIMON LOSE -> zamykam minigrę");
            StopGame();
        };

        _minigame.StartRun();
    }

    public void StopGame()
    {
        if (_minigame != null) _minigame.StopRun();
        if (_menuCanvas != null) _menuCanvas.SetActive(false);
    }
}