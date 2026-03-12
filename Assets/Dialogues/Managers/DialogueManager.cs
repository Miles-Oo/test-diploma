using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string npcTag = "Npc";
    [TextArea]
    public string defaultDialogue = "Cześć!";

    private bool inDialogue = false;
    private GameObject currentNPCObject;

    public GameObject CurrentNPC => currentNPCObject;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!inDialogue && currentNPCObject != null && Input.GetKeyDown(KeyCode.E))
            StartDialogue(currentNPCObject);
        else if (inDialogue && Input.GetKeyDown(KeyCode.E))
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
        dialoguePanel.SetActive(true);

        var npcDialogue = npcObj.GetComponent<NPCDialogue>();
        dialogueText.text = npcDialogue != null ? npcDialogue.dialogueText : defaultDialogue;

        FreezeCharacters(true);

        Debug.Log("Rozpoczęto dialog z: " + npcObj.name);
    }

    public void EndDialogue()
    {
        if (!inDialogue) return;

        inDialogue = false;
        dialoguePanel.SetActive(false);
        currentNPCObject = null;
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
}