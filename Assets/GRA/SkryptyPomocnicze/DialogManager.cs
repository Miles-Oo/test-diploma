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

    // blokada interakcji po wyjściu z dialogu
    private float interactionBlockTimer = 0f;
    public bool IsInteractionBlocked => interactionBlockTimer > 0f;

    private System.Action onDialogEnd;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialogUI.SetActive(false);
        continueButton.onClick.AddListener(EndDialog);
    }

    void Update()
    {
        if (interactionBlockTimer > 0f)
            interactionBlockTimer -= Time.deltaTime;

        if (_inDialog && Input.GetKeyDown(KeyCode.E))
        {
            EndDialog();
        }
    }

    public void StartDialog(string text, System.Action onEnd = null)
    {
        dialogUI.SetActive(true);
        dialogText.text = text;
        _inDialog = true;
        onDialogEnd = onEnd;

        FreezeCharacters(true);
    }

    public void EndDialog()
    {
        dialogUI.SetActive(false);
        _inDialog = false;

        // blokujemy interakcje na chwilę
        interactionBlockTimer = 0.2f;

        FreezeCharacters(false);

        onDialogEnd?.Invoke();
    }

    private void FreezeCharacters(bool freeze)
    {
        // gracz
        var player = GameObject.FindWithTag("Player")?.GetComponent<PlayerMovement>();
        if (player != null) player.enabled = !freeze;

        // NPC
        var npcs = FindObjectsOfType<NpcMovement>();

        foreach (var npc in npcs)
        {
            npc.m_canWalk = !freeze;
        }
    }
}