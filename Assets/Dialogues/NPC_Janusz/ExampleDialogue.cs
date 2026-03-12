// using UnityEngine;

// public class ExampleDialogue : MonoBehaviour, IInteractable
// {
//     public DialogueNode startNode;

//     void Start()
//     {
//         CreateDialogue();
//     }

//     void CreateDialogue()
//     {
//         DialogueNode start = new DialogueNode();
//         start.npcText = "Witaj podróżniku. Czego potrzebujesz?";

//         DialogueNode who = new DialogueNode();
//         who.npcText = "Jestem strażnikiem tej wioski.";

//         DialogueNode trade = new DialogueNode();
//         trade.npcText = "Niestety dziś nic nie sprzedaję.";

//         start.choices.Add(new DialogueChoice { choiceText = "Kim jesteś?", nextNode = who });
//         start.choices.Add(new DialogueChoice { choiceText = "Masz coś na sprzedaż?", nextNode = trade });
//         start.choices.Add(new DialogueChoice { choiceText = "Żegnaj", nextNode = null });

//         startNode = start;
//     }

//     public void Interact()
//     {
//         DialogueManager.Instance.StartDialogue(startNode);
//     }
// }