using UnityEngine;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    [TextArea]
    public string dialogueText = "Cześć!";
    public DialogueNode startNode;
    [HideInInspector]
    public DialogueNode lastNodeUsed = null;
    public Sprite portrait;

    public void Interact()
    {
        var dm = DialogueManager.Instance;
        if (dm == null)
            return;

        if (dm.IsInDialogue() && dm.CurrentNPC == this.gameObject)
        {
            dm.EndDialogue();
            return;
        }

        if (dm.IsInDialogue() && dm.CurrentNPC != this.gameObject)
            return;

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