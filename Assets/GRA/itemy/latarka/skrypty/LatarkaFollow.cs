using UnityEngine;
public class LatarkaFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Update()
    {
        //nie skracać tego bo wtedy vector3 a nie 2 jest użyty i pozycja z jest na -10 a nie 0
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       transform.position=mousePosition;
    }
}
