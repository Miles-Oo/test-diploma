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
        if (Input.GetKeyDown(KeyCode.E)){
        print("AHA.");
        if(_interactable != null){
            if (isInteracting){ 
                isInteracting=false;
                print("kończę");
                _interactable.TurnOFFInteract();
            }else{ 
                isInteracting=true;
                print("zaczynam");  
                _interactable.TurnONInteract();
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(_interactable==null){
       _interactable=other.GetComponent<IInteractable>();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(isInteracting) return;
        if(_interactable!=null){
        _interactable=null;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
