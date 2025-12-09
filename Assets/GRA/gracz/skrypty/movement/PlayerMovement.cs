using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private InputAction moveAction;
    private float moveSpeed = 2f;
    private Vector2 moveInput;

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
        _rb.linearVelocity = moveInput * moveSpeed;
    }  

    
    private void OnEnable(){ moveAction.Enable();}

    private void OnDisable(){ moveAction.Disable();}

}
