using UnityEngine;
using System.Collections.Generic;
public class NPCDialogueTest : MonoBehaviour, IInteractable
{
    [TextArea]
    public string dialogueText = "Cześć!";
    public DialogueAsset dialogueAsset;
    [HideInInspector]
    public DialogueNode lastNodeUsed = null;
    public Sprite portrait;
    public List<DialogueAsset> availableDialogues;
    
    public int currentDialogueIndex = 0;

    public void SetNextDialogue()
    {
        if (currentDialogueIndex < availableDialogues.Count - 1)
        {
            currentDialogueIndex++;
            lastNodeUsed = null; 
        }
    }

    public void Interact(GameObject gameObject,InteractorType interactor)
    {
        if(interactor != InteractorType.Gracz)
            return;
            
        var dm = DialogueManagerTest.Instance;
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
            DialogueManagerTest.Instance.ClearCurrentNPC(this.gameObject);
        }
    }
}