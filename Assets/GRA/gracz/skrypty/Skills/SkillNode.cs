using UnityEngine;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour
{
    [SerializeField] private SkillType skillType;
    [SerializeField] private int cost = 1;
    [Header("Requirement")]
    [SerializeField] private SkillType requiredSkill;

    [SerializeField] private Button button;
    [SerializeField] private leveling _leveling;

    private bool isUnlocked = false;

    private void Start()
    {
        button.onClick.AddListener(UnlockSkill);
    }

    public void SetState(int skillPoints)
    {
        if (isUnlocked)
        {
            button.interactable = false;
            return;
        }

        bool hasRequirement =
            requiredSkill == skillType || PlayerSkills.Instance.Has(requiredSkill);

        button.interactable =
            skillPoints >= cost && hasRequirement;
    }

    private void UnlockSkill()
    {
        if (isUnlocked) return;

        if (!_leveling.SpendSkillPoint(cost))
            return;

        PlayerSkills.Instance.Unlock(skillType);

        isUnlocked = true;

        button.interactable = false;

        Debug.Log("Unlocked skill: " + skillType);
    }
}