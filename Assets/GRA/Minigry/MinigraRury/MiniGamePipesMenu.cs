using UnityEngine;

public class MiniGamePipesMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private PipePuzzleUIMinigame _minigame;

    public GameObject GetMenuCanvas() => _menuCanvas;

    private void Awake()
    {
        if (_menuCanvas != null) _menuCanvas.SetActive(false);
        if (_minigame != null) _minigame.enabled = false;
    }

    public void StartGame()
{
    _menuCanvas.SetActive(true);

    // podpinamy reakcję na win
    _minigame.OnWin += () =>
    {
        Debug.Log("WIN -> zamykam minigrę");
        StopGame(); // chowa canvas i zatrzymuje logikę
        // tu możesz też dać: nagroda, odblokowanie prądu itd.
    };

    _minigame.StartRun();
}


    public void StopGame()
    {
        if (_minigame != null) _minigame.StopRun();
        if (_menuCanvas != null) _menuCanvas.SetActive(false);
    }
}
