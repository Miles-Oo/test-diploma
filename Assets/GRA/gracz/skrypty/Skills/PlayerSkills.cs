using UnityEngine;
using System.Collections.Generic;

public class PlayerSkills : MonoBehaviour
{
    public static PlayerSkills Instance;

    private HashSet<SkillType> unlocked = new HashSet<SkillType>();

    private void Awake()
    {
        Instance = this;
    }

    public void Unlock(SkillType type)
    {
        unlocked.Add(type);
    }

    public bool Has(SkillType type)
    {
        return unlocked.Contains(type);
    }

    public int ModifyHungerGain(int baseValue)
    {
        if (!Has(SkillType.BetterMetabolism))
            return baseValue;

        float modified = baseValue * 1.10f;
        return Mathf.RoundToInt(modified);
    }
}