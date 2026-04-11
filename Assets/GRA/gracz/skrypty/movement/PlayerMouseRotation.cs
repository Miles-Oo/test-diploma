using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

void FixedUpdate()
{
    Vector3 mouseWorld3 = cam.ScreenToWorldPoint(Input.mousePosition);
    Vector2 mouseWorld = (Vector2)mouseWorld3;

    Vector2 dir = mouseWorld - rb.position;
    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

    rb.MoveRotation(angle);
}

}
