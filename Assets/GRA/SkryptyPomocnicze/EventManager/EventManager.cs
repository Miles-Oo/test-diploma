using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private Dictionary<string, IUnlockableMiniGame> miniGames = new Dictionary<string, IUnlockableMiniGame>();
    private Dictionary<string, GameObject> questMarks = new Dictionary<string, GameObject>();
    private Dictionary<string, Transform> miniGameTargets = new Dictionary<string, Transform>();

    [SerializeField] private GameObject questMarkPrefab;

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

    public void RegisterMiniGameTarget(string id, Transform target)
    {
        if (!miniGameTargets.ContainsKey(id))
            miniGameTargets.Add(id, target);
    }

    public void UnlockMiniGame(string id)
    {
        if (miniGames.TryGetValue(id, out var miniGame))
        {
            miniGame.UnlockMiniGame();

            if (miniGameTargets.TryGetValue(id, out var target) && target != null)
            {
                SpawnQuestMark(id, target);
            }
        }
    }

    public void LockMiniGame(string id)
    {
        if (questMarks.TryGetValue(id, out var mark) && mark != null)
        {
            Destroy(mark);
            questMarks.Remove(id);
        }
    }

    private void SpawnQuestMark(string id, Transform target)
    {
        if (questMarkPrefab == null)
        {
            Debug.LogError("Brak questMarkPrefab!");
            return;
        }

        if (questMarks.ContainsKey(id))
            return;

        GameObject mark = Instantiate(questMarkPrefab);

        QuestMark qm = mark.GetComponent<QuestMark>();
        if (qm != null)
        {
            qm.SetTarget(target);
        }

        questMarks.Add(id, mark);
    }

    public void RemoveQuestMark(string id)
    {
        if (questMarks.TryGetValue(id, out var mark))
        {
            Destroy(mark);
            questMarks.Remove(id);
        }
    }
}