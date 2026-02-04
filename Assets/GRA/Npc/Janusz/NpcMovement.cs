using UnityEngine;
public class NpcMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    private float moveSpeed = 3f;
    private Vector2 moveInput;
    public void moveFor(Vector2 vector2)
    {
        moveInput = vector2;
    }

    public bool m_canWalk=true;

     private bool m_Moving=false;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
    }

    public Vector2 getCurrPos()
    {
        return _rb.transform.position;
    }
    public bool IsMoving(){return m_Moving;}
    void FixedUpdate()
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

}
