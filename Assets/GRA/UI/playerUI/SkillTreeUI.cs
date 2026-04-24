using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    [SerializeField] private leveling _leveling;

    [Header("UI")]
    [SerializeField] private GameObject skillButton;
    [SerializeField] private GameObject skillCanvas;
    [SerializeField] private Button closeButton;

    [Header("Nodes")]
    [SerializeField] private SkillNode[] skillNodes;
    private bool isOpen = false;
    private void Start()
    {
        skillCanvas.SetActive(false);

        _leveling.OnSkillPointsChanged += UpdateUI;
        _leveling.OnExpChange += UpdateUI;

        closeButton.onClick.AddListener(CloseSkillTree);

        UpdateUI();
    }

    private void UpdateUI()
    {
        int points = _leveling.getSkillPoints();

        skillButton.SetActive(points > 0 && !isOpen);

        foreach (var node in skillNodes)
        {
            if (node != null)
                node.SetState(points);
        }
    }

    public void OpenSkillTree()
    {
        isOpen = true;
        skillCanvas.SetActive(true);

        UpdateUI();
    }

    public void CloseSkillTree()
    {
        isOpen = false;
        skillCanvas.SetActive(false);

        UpdateUI();
    }

    private void OnDestroy()
    {
        _leveling.OnSkillPointsChanged -= UpdateUI;
        _leveling.OnExpChange -= UpdateUI;

        if (closeButton != null)
            closeButton.onClick.RemoveAllListeners();
    }
}