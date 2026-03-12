// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System.Collections.Generic;

// public class DialogueManager : MonoBehaviour
// {
//     public static DialogueManager Instance;

//     [Header("UI Elements")]
//     public GameObject dialogUI;
//     public TextMeshProUGUI dialogText;

//     public Button choiceButton1;
//     public Button choiceButton2;
//     public Button choiceButton3;

//     private DialogueNode currentNode;

//     private bool _inDialog = false;
//     public bool inDialog => _inDialog;

//     private float interactionBlockTimer = 0f;
//     public bool IsInteractionBlocked => interactionBlockTimer > 0f;

//     void Awake()
//     {
//         if (Instance == null) Instance = this;
//         else Destroy(gameObject);

//         dialogUI.SetActive(false);
//     }

//     void Update()
//     {
//         if (interactionBlockTimer > 0f)
//             interactionBlockTimer -= Time.deltaTime;
//     }

//     public void StartDialogue(DialogueNode startNode)
//     {
//         dialogUI.SetActive(true);
//         _inDialog = true;

//         FreezeCharacters(true);

//         ShowNode(startNode);
//     }

//     void ShowNode(DialogueNode node)
//     {
//         currentNode = node;
//         dialogText.text = node.npcText;

//         SetupChoice(choiceButton1, 0);
//         SetupChoice(choiceButton2, 1);
//         SetupChoice(choiceButton3, 2);
//     }

//     void SetupChoice(Button button, int index)
//     {
//         if (currentNode.choices.Count > index)
//         {
//             button.gameObject.SetActive(true);

//             var choice = currentNode.choices[index];
//             button.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;

//             button.onClick.RemoveAllListeners();
//             button.onClick.AddListener(() =>
//             {
//                 if (choice.nextNode != null)
//                     ShowNode(choice.nextNode);
//                 else
//                     EndDialog();
//             });
//         }
//         else
//         {
//             button.gameObject.SetActive(false);
//         }
//     }

//     public void EndDialog()
//     {
//         dialogUI.SetActive(false);
//         _inDialog = false;

//         interactionBlockTimer = 0.2f;

//         FreezeCharacters(false);
//     }

//     private void FreezeCharacters(bool freeze)
//     {
//         var player = GameObject.FindWithTag("Player")?.GetComponent<PlayerMovement>();
//         if (player != null) player.enabled = !freeze;

//         var npcs = FindObjectsOfType<NpcMovement>();

//         foreach (var npc in npcs)
//         {
//             npc.m_canWalk = !freeze;
//         }
//     }
// }