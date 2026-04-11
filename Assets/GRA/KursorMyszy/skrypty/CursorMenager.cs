using UnityEngine;

public class CursorMenager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 clikpos=Vector2.zero;
    void Start()
    {
        Cursor.SetCursor(cursorTexture,clikpos,CursorMode.Auto);
    }
}
