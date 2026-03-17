using UnityEngine;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    [TextArea]
    public string dialogueText = "Cześć!";
    public DialogueNode startNode; // początkowy węzeł dialogu
    [HideInInspector]
    public DialogueNode lastNodeUsed = null;

    // Ta metoda jest wywoływana przez Twój interactions
    public void Interact()
    {
        var dm = DialogueManager.Instance;
        if (dm == null)
            return;

        // jeśli jesteśmy w dialogu z tym samym NPC → zakończ dialog
        if (dm.IsInDialogue() && dm.CurrentNPC == this.gameObject)
        {
            dm.EndDialogue();
            return;
        }

        // jeśli jesteśmy w dialogu z innym NPC → ignoruj
        if (dm.IsInDialogue() && dm.CurrentNPC != this.gameObject)
            return;

        // jeśli nie jesteśmy w dialogu → rozpocznij dialog
        dm.StartDialogue(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.ClearCurrentNPC(this.gameObject);
        }
    }
}