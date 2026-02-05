using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NpcRotation : MonoBehaviour
{
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 v = _rb.linearVelocity;

        // jeśli stoi — nie obracamy
        if (v.sqrMagnitude < 0.0001f)
            return;

        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 90f;
        _rb.MoveRotation(angle);
    }
}
