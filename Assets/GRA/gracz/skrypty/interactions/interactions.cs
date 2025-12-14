using UnityEngine;
public class interactions:MonoBehaviour{
    
    [SerializeField] private BoxCollider2D _fieldAction;
    private IInteractable _interactiveItem=null;

    private void Update(){
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(_interactiveItem!=null){
            _interactiveItem.Interact();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
    var interactable = other.GetComponent<IInteractable>();
    if (interactable != null && _interactiveItem == null)
    {
        print("widze");
        _interactiveItem = interactable;
    }
    }
    void OnTriggerExit2D(Collider2D other)
    {
    var interactable = other.GetComponent<IInteractable>();
    if (interactable == _interactiveItem)
    {
        print("nie widze");
        _interactiveItem = null;
    }
    }

    private void FixedUpdate()
    {
        
    }
}
