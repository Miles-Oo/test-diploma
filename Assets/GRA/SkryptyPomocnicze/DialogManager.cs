using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [Header("UI Elements")]
    public GameObject dialogUI;
    public TextMeshProUGUI dialogText;
    public Button continueButton;

    private bool _inDialog = false;
    public bool inDialog => _inDialog;

    private float interactionBlockTimer = 0f;
    public bool IsInteractionBlocked => interactionBlockTimer > 0f;

    private string[] currentLines;
    private int currentLineIndex;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        dialogUI.SetActive(false);
        continueButton.onClick.AddListener(NextLine);
    }

    void Update()
    {
        if (interactionBlockTimer > 0f)
            interactionBlockTimer -= Time.deltaTime;
    }

    public void StartDialog(string[] lines)
    {
        currentLines = lines;
        currentLineIndex = 0;

        _inDialog = true; // ustaw od razu, żeby Update() wiedział, że jesteśmy w dialogu
        dialogUI.SetActive(true);
        dialogText.text = currentLines[currentLineIndex];

        FreezeCharacters(true);
    }

    private void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex >= currentLines.Length)
        {
            EndDialog();
        }
        else
        {
            dialogText.text = currentLines[currentLineIndex];
        }
    }

    public void EndDialog()
    {
        dialogUI.SetActive(false);
        _inDialog = false;
        interactionBlockTimer = 0.2f;
        FreezeCharacters(false);
    }

    private void FreezeCharacters(bool freeze)
    {
        var player = GameObject.FindWithTag("Player")?.GetComponent<PlayerMovement>();
        if (player != null)
            player.enabled = !freeze;

        var npcs = FindObjectsOfType<NpcMovement>();
        foreach (var npc in npcs)
        {
            npc.m_canWalk = !freeze;
        }
    }
}