using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string npcTag = "NPC";
    [TextArea]
    public string defaultDialogue = "Cześć!";

    private bool inDialogue = false;
    private GameObject currentNPCObject;

    public GameObject CurrentNPC => currentNPCObject;

    private int dialogueStartFrame;

    public Transform optionsContainer;
    public GameObject optionButtonPrefab;

    private DialogueNode currentNode;

    public GameObject dmBackground;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
        if (dmBackground != null)
            dmBackground.SetActive(false);
    }

    private void Update()
    {
        if (!inDialogue && currentNPCObject != null && Input.GetKeyDown(KeyCode.E))
            StartDialogue(currentNPCObject);
        else if (inDialogue && Input.GetKeyDown(KeyCode.E) && Time.frameCount > dialogueStartFrame)
            EndDialogue();
    }

    public void SetCurrentNPC(GameObject obj)
    {
        if (obj.CompareTag(npcTag))
            currentNPCObject = obj;
    }

    public void ClearCurrentNPC(GameObject obj)
    {
        if (currentNPCObject == obj)
            currentNPCObject = null;
    }

    public void StartDialogue(GameObject npcObj)
    {
        if (inDialogue) return;

        inDialogue = true;
        dialogueStartFrame = Time.frameCount;
        dialoguePanel.SetActive(true);
        dmBackground.SetActive(true);

        currentNPCObject = npcObj;
        var npcDialogue = npcObj.GetComponent<NPCDialogue>();

        if (npcDialogue != null && npcDialogue.startNode != null)
        {
            DialogueNode nodeToShow = npcDialogue.lastNodeUsed;

            // Jeśli checkpoint jest zły -> wróć do startNode
            if (nodeToShow == null || nodeToShow.options == null || nodeToShow.options.Length == 0)
            {
                nodeToShow = npcDialogue.startNode;
            }

            ShowNode(nodeToShow);
        }
        else
        {
            dialogueText.text = defaultDialogue;
        }
        FreezeCharacters(true);

        Debug.Log("Rozpoczęto dialog z: " + npcObj.name);
    }

    public void EndDialogue()
    {
        if (!inDialogue) return;

        inDialogue = false;
        dialoguePanel.SetActive(false);
        dmBackground.SetActive(false);
        // currentNPCObject = null;
        FreezeCharacters(false);

        Debug.Log("Zakończono dialog");
    }

    public bool IsInDialogue()
    {
        return inDialogue;
    }

    private void FreezeCharacters(bool freeze)
    {
        // blokada gracza
        var player = GameObject.FindWithTag("Player")?.GetComponent<PlayerMovement>();
        if (player != null) player.enabled = !freeze;

        // blokada NPC
        var npcs = FindObjectsOfType<NpcMovement>();
        foreach (var npc in npcs)
        {
            npc.m_canWalk = !freeze;
        }
    }

    public void ShowNode(DialogueNode node)
    {
        currentNode = node;

        dialogueText.text = node.npcText;

        // usuń stare opcje
        foreach (Transform child in optionsContainer)
        {
            Destroy(child.gameObject);
        }
        if (node.options == null || node.options.Length == 0)
            return;

        // stwórz nowe
        foreach (var option in node.options)
        {
            GameObject btnObj = Instantiate(optionButtonPrefab, optionsContainer);
            btnObj.SetActive(true);

            var button = btnObj.GetComponent<UnityEngine.UI.Button>();
            button.enabled = true;

            TMP_Text btnText = btnObj.GetComponentInChildren<TMP_Text>();
            btnText.enabled = true;
            btnText.text = option.optionText;

            button.onClick.AddListener(() =>
            {
                ChooseOption(option);
            });
        }
    }

    void ChooseOption(DialogueOption option)
    {
        if (currentNPCObject != null)
        {
            var npcDialogue = currentNPCObject.GetComponent<NPCDialogue>();
            if (npcDialogue != null)
            {
                // zapisujemy checkpoint tylko jeśli nextNode istnieje
                npcDialogue.lastNodeUsed = currentNode;
            }
        }
        if (option.nextNode != null)
            ShowNode(option.nextNode);
        else
            EndDialogue();
    }
}