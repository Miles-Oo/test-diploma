using UnityEngine;
public class interactions:MonoBehaviour{
    
    [SerializeField] private BoxCollider2D _fieldAction;

    private bool isInteracting=false;
    private IInteractable _interactable;
    private void Awake()
    {
     
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInteracting)
            { 
                if(_interactable != null){
                isInteracting=false;
                print("kończę");
                _interactable.TurnOFFInteract();
                }
            }
            else
            { 
                 if(_interactable != null){
                isInteracting=true;
                print("zaczynam");  
                _interactable.TurnONInteract();
                 }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       _interactable=other.GetComponent<IInteractable>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        _interactable=null;
    }

    private void FixedUpdate()
    {
        
    }
}
