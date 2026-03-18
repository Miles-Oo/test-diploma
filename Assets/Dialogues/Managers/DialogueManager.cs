using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public System.Action<GameObject> OnDialogueEnded;
    public Image portraitImage;

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

        // --- nowa część: obsługa klawiszy 1,2,3 ---
        if (inDialogue && currentNode != null && currentNode.options != null)
        {
            for (int i = 0; i < currentNode.options.Length && i < 9; i++)
            {
                KeyCode key = KeyCode.Alpha1 + i; // Alpha1 = "1", Alpha2 = "2", ...
                if (Input.GetKeyDown(key))
                {
                    ChooseOption(currentNode.options[i]);
                    break;
                }
            }
        }
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

        if (portraitImage != null)
        {
            if (npcDialogue != null && npcDialogue.portrait != null)
            {
                portraitImage.sprite = npcDialogue.portrait;
                portraitImage.gameObject.SetActive(true);
            }
            else
            {
                portraitImage.gameObject.SetActive(false);
            }
        }

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

        if (portraitImage != null)
        {
            portraitImage.gameObject.SetActive(false);
        }

        Debug.Log("Zakończono dialog");

    }

    public bool IsInDialogue()
    {
        return inDialogue;
    }

    private void FreezeCharacters(bool freeze)
    {
        var player = GameObject.FindWithTag("Player")?.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.enabled = !freeze;

            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null && freeze)
            {
                rb.linearVelocity = Vector2.zero;
                // rb.angularVelocity = 0f; 
            }
        }

        var npcs = FindObjectsOfType<NpcMovement>();
        foreach (var npc in npcs)
        {
            npc.m_canWalk = !freeze;

            var rb = npc.GetComponent<Rigidbody2D>();
            if (rb != null && freeze)
            {
                rb.linearVelocity = Vector2.zero;
                // rb.angularVelocity = 0f;
            }
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
        for (int i = 0; i < node.options.Length; i++)
        {
            var option = node.options[i];

            GameObject btnObj = Instantiate(optionButtonPrefab, optionsContainer);
            btnObj.SetActive(true);

            var button = btnObj.GetComponent<UnityEngine.UI.Button>();
            TMP_Text btnText = btnObj.GetComponentInChildren<TMP_Text>();

            btnText.text = $"{i + 1}. {option.optionText}"; // dodaj numerkę

            int optionIndex = i; // potrzebne do lambdy
            button.onClick.AddListener(() =>
            {
                ChooseOption(node.options[optionIndex]);
            });
        }
    }

    void ChooseOption(DialogueOption option)
    {
        if (option.unlockMiniGame != null)
        {
            option.unlockMiniGame.UnlockMiniGame();
        }
        if (currentNPCObject != null)
        {
            var npcDialogue = currentNPCObject.GetComponent<NPCDialogue>();
            if (npcDialogue != null && option.rememberNode)
            {
                npcDialogue.lastNodeUsed = currentNode;
            }
        }
        if (option.nextNode != null)
            ShowNode(option.nextNode);
        else
            EndDialogue();
    }
}