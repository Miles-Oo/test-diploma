using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameBezpiecznikiController : MonoBehaviour
{
    [SerializeField] private List<MiniGameBezpiecznikiButton> bezpieczniki;
    [SerializeField] private float startTimeToClick = 0.5f;
    [SerializeField] private float minTimeToClick = 0.3f;
    [SerializeField] private float speedUpPerSuccess = 0.01f;

    private float currentTimeToClick;

    private MiniGameBezpiecznikiButton current;
    private Coroutine gameLoop;

    public System.Action OnGameFinished;
    public System.Action OnExitRequested;



    // private void OnEnable()
    // {
    //     StartGame();
    // }

    public void StartGame()
    {
        ResetGame();

        currentTimeToClick = startTimeToClick;

        foreach (var b in bezpieczniki)
            b.Init(this);

        gameLoop = StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (!AllSolved())
        {
            current = GetRandomUnsovled();
            current.Activate();

            float timer = 0f;
            bool clicked = false;

            while (timer < currentTimeToClick)
            {
                timer += Time.deltaTime;
                yield return null;

                if (current.IsSolved())
                {
                    clicked = true;
                    break;
                }
                yield return null;
            }

            if (clicked)
            {
                currentTimeToClick = Mathf.Max(
                    minTimeToClick,
                    currentTimeToClick - speedUpPerSuccess
                );
            }
            else
            {
                current.Deactivate();
            }
            yield return new WaitForSeconds(0.02f);
        }

        GameWon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExitRequested?.Invoke();
        }
    }

    public void PlayerClicked(MiniGameBezpiecznikiButton button)
    {
        if (button == current)
            button.Solve();
    }

    bool AllSolved()
    {
        foreach (var b in bezpieczniki)
            if (!b.IsSolved()) return false;
        return true;
    }

    MiniGameBezpiecznikiButton GetRandomUnsovled()
    {
        List<MiniGameBezpiecznikiButton> available = bezpieczniki.FindAll(b => !b.IsSolved());
        return available[Random.Range(0, available.Count)];
    }

    public void ResetGame()
    {
        if (gameLoop != null)
        {
            StopCoroutine(gameLoop);
            gameLoop = null;
        }

        current = null;

        foreach (var b in bezpieczniki)
            b.ResetState();
    }


    void GameWon()
    {
        Debug.Log("MINIGRA UKOŃCZONA!");
        OnGameFinished?.Invoke();
        ResetGame();
        //TurnOFFInteract();
    }
}
