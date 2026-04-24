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
        float multiplier = 1f;

        if (Has(SkillType.Metabolism_I)) multiplier += 0.05f;
        if (Has(SkillType.Metabolism_II)) multiplier += 0.1f;
        if (Has(SkillType.Metabolism_III)) multiplier += 0.15f;
        if (Has(SkillType.Metabolism_IV)) multiplier += 0.2f;
        if (Has(SkillType.Metabolism_V)) multiplier += 0.25f;

        return Mathf.RoundToInt(baseValue * multiplier);
    }

    public int ModifyEnergyGain(int baseValue)
    {
        float multiplier = 1f;

        if (Has(SkillType.Energy_I)) multiplier += 0.05f;
        if (Has(SkillType.Energy_II)) multiplier += 0.1f;
        if (Has(SkillType.Energy_III)) multiplier += 0.15f;
        if (Has(SkillType.Energy_IV)) multiplier += 0.2f;
        if (Has(SkillType.Energy_V)) multiplier += 0.25f;

        return Mathf.RoundToInt(baseValue * multiplier);
    }
}