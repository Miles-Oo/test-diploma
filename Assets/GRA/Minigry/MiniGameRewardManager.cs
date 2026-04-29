using UnityEngine;
using System.Collections.Generic;

public class MiniGameRewardManager : MonoBehaviour
{
    public static MiniGameRewardManager Instance;

    [SerializeField] private leveling _leveling;

    private Dictionary<string, int> miniGameCompletions = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GiveReward(string miniGameID, float baseExp)
    {
        if (!miniGameCompletions.ContainsKey(miniGameID))
            miniGameCompletions[miniGameID] = 0;

        miniGameCompletions[miniGameID]++;

        int completionCount = miniGameCompletions[miniGameID];

        float finalExp = CalculateExp(baseExp, completionCount);

        _leveling.addExp(Mathf.FloorToInt(finalExp));

        Debug.Log($"Minigame {miniGameID} | run: {completionCount} | EXP: {finalExp}");
    }

    private float CalculateExp(float baseExp, int count)
    {
        float multiplier = Mathf.Min(count, 5);

        float lvlBonus = _leveling.getLvl() * 1.3f;

        return baseExp * multiplier + lvlBonus;
    }
}