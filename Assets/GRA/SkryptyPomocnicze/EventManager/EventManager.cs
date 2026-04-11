using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private Dictionary<string, IUnlockableMiniGame> miniGames = new Dictionary<string, IUnlockableMiniGame>();
    private Dictionary<string, GameObject> questMarks = new Dictionary<string, GameObject>();
    private Dictionary<string, Transform> miniGameTargets = new Dictionary<string, Transform>();

    [SerializeField] private Vector3 questMarkOffset = new Vector3(0, 0.2f, 0);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterMiniGame(string id, IUnlockableMiniGame miniGame)
    {
        if (!miniGames.ContainsKey(id))
            miniGames.Add(id, miniGame);
    }

    public void RegisterQuestMark(string id, GameObject questMark)
    {
        if (questMark == null) return;

        if (!questMarks.ContainsKey(id))
        {
            questMarks.Add(id, questMark);
            questMark.SetActive(false);
        }
    }

    public void RegisterMiniGameTarget(string id, Transform target)
    {
        if (!miniGameTargets.ContainsKey(id))
            miniGameTargets.Add(id, target);
    }

    private void LateUpdate()
    {
        foreach (var kvp in questMarks)
        {
            string id = kvp.Key;
            GameObject mark = kvp.Value;

            if (mark == null) continue;

            if (miniGameTargets.TryGetValue(id, out var target) && target != null)
            {
                mark.transform.position = target.position + questMarkOffset;
            }
        }
    }

    public void UnlockMiniGame(string id)
    {
        if (miniGames.TryGetValue(id, out var miniGame))
        {
            miniGame.UnlockMiniGame();

        if (questMarks.TryGetValue(id, out var mark) && mark != null)
            mark.SetActive(true);
        }
        else
        {
        }
    }

    public void LockMiniGame(string id)
    {
        if (miniGames.TryGetValue(id, out var miniGame))
            miniGame.LockMiniGame();

        if (questMarks.TryGetValue(id, out var mark) && mark != null)
            mark.SetActive(false);
    }
}