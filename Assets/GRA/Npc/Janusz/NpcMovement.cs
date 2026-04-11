using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    private float moveSpeed = 3f;
    private Vector2 moveInput;

    public bool m_canWalk = true;

    private bool m_Moving = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void moveFor(Vector2 vector2)
    {
        moveInput = vector2;
    }

    public Vector2 getCurrPos()
    {
        return _rb.transform.position;
    }

    public bool IsMoving()
    {
        return m_Moving;
    }

    void FixedUpdate()
    {
        if (m_canWalk)
        {
            _rb.linearVelocity = moveInput * moveSpeed;
        }
        else
        {
            // zatrzymanie NPC w miejscu
            _rb.linearVelocity = Vector2.zero;
        }

        m_Moving = _rb.linearVelocity != Vector2.zero;
    }
}