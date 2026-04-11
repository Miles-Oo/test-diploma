using UnityEngine;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    [TextArea]
    public string dialogueText = "Cześć!";
    public DialogueNode startNode;
    [HideInInspector]
    public DialogueNode lastNodeUsed = null;
    public Sprite portrait;

    public void Interact(GameObject gameObject,InteractorType interactor)
    {
        if(interactor != InteractorType.Gracz)
            return;
            
        var dm = DialogueManager.Instance;
        if (dm == null)
            return;

        if (!dm.CanInteract(this)) //
            return;
        
        if (UnityEngine.EventSystems.EventSystem.current != null && //////////////
            UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        if (dm.IsInDialogue() && dm.CurrentNPC == this.gameObject)
        {
            dm.EndDialogue();
            dm.ClearCurrentInteractable(this); //
            return;
        }

        if (dm.IsInDialogue() && dm.CurrentNPC != this.gameObject)
            return;
        
        dm.SetCurrentInteractable(this); //
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