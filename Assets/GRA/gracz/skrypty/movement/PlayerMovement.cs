using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private InputAction moveAction;
    private float moveSpeed = 4f;
    private Vector2 moveInput;

    public bool m_canWalk=true;

     private bool m_Moving=false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    public bool IsMoving(){return m_Moving;}
    private void FixedUpdate()
    {
        if(m_canWalk){
        _rb.linearVelocity = moveInput * moveSpeed;
        }
        if(_rb.linearVelocity.x==0&&_rb.linearVelocity.y==0)
        {
            m_Moving=false;
        }
        else
        {
            m_Moving=true;
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
