using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private InputAction moveAction;
    private float moveSpeed = 4f;
    private Vector2 moveInput;

    public bool m_canWalk=true;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if(m_canWalk){
        _rb.linearVelocity = moveInput * moveSpeed;
        }
    }  

    public void CanMove(bool canMoo)
    {
        if (canMoo)
        {
            moveAction.Enable();
        }
        else
        {
            moveAction.Disable();
            _rb.linearVelocity=Vector2.zero;
        }
        
    }
    private void OnEnable(){ moveAction.Enable();}

    private void OnDisable(){ moveAction.Disable();}

}
