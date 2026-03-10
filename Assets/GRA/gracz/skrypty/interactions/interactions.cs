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
    if (DialogManager.Instance != null)
    {
        // jeśli dialog trwa
        if (DialogManager.Instance.inDialog)
        {
            if (Input.GetKeyDown(KeyCode.E))
                DialogManager.Instance.EndDialog();

            return;
        }

        // jeśli właśnie wyszliśmy z dialogu
        if (DialogManager.Instance.IsInteractionBlocked)
        {
            return;
        }
    }

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

    najblizy.Interact();
}

void OnTriggerEnter2D(Collider2D other)
{
    var interactable = other.GetComponent<IInteractable>();
    if (interactable != null && !_interactiveItem.Contains(interactable))
    {
        _interactiveItem.Add(interactable);
    }
}

void OnTriggerExit2D(Collider2D other)
{
    var interactable = other.GetComponent<IInteractable>();
    if (interactable != null)
    {
        _interactiveItem.Remove(interactable);
    }
}

}
