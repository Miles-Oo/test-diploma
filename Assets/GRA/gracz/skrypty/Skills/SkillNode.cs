using UnityEngine;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour
{
    [SerializeField] private SkillType skillType;
    [SerializeField] private int cost = 1;

    [SerializeField] private Button button;
    [SerializeField] private leveling _leveling;

    private bool isUnlocked = false;

    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(UnlockSkill);
    }

    // 👉 sterowanie tylko z SkillTreeUI
    public void SetState(int skillPoints)
    {
        if (isUnlocked)
        {
            button.interactable = false;
            return;
        }

        button.interactable = skillPoints >= cost;
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