using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class interactions:MonoBehaviour{
    
    private BoxCollider2D _fieldAction;
    private List<IInteractable>  _interactiveItem=new();



void Awake()
    {
        _fieldAction=GetComponent<BoxCollider2D>();
    }
private void Update()
{
    if (DialogueManager.Instance != null && DialogueManager.Instance.IsInDialogue())
        return;

    if (Input.GetKeyDown(KeyCode.E) && _interactiveItem.Count > 0)
    {
        rawr();
    }
}

private void rawr()
{
    if (_interactiveItem.Count == 0) return;

    var najblizy = _interactiveItem[0];

    for (int i = 1; i < _interactiveItem.Count; i++)
    {
        var a = _interactiveItem[i] as MonoBehaviour;
        var b = najblizy as MonoBehaviour;

        if (a == null || b == null) continue;

        float da = Vector2.Distance(transform.position, a.transform.position);
        float db = Vector2.Distance(transform.position, b.transform.position);

        if (da < db)
            najblizy = _interactiveItem[i];
    }

    najblizy.Interact(gameObject, InteractorType.Gracz);
}

void OnTriggerEnter2D(Collider2D other)
{
    if (DialogueManager.Instance != null && DialogueManager.Instance.IsInDialogue())
        return;

    var interactable = other.GetComponent<IInteractable>();
    if (interactable != null && !_interactiveItem.Contains(interactable))
    {
        _interactiveItem.Add(interactable);
        Debug.Log("Entered trigger with: " + other.name);
    }
}

void OnTriggerExit2D(Collider2D other)
{
    var interactable = other.GetComponent<IInteractable>();
    if (interactable != null)
    {
        _interactiveItem.Remove(interactable);
        Debug.Log("Exited trigger with: " + other.name);
    }
}

}
