using UnityEngine;

public class NPCDialog : MonoBehaviour, IInteractable
{
    [TextArea]
    public string[] dialogLines;

    public void Interact()
    {
        if (dialogLines.Length > 0)
        {
            DialogManager.Instance.StartDialog(dialogLines[0]);
        }
    }
}