using UnityEngine;

public class Computer_Mouse : MonoBehaviour
{
    public Texture2D ComputerCursor;
    void SetCursor()
    {
        Cursor.visible = true;
        Cursor.SetCursor(ComputerCursor, Vector2.zero, CursorMode.Auto);
    }

    void Start()
    {
        SetCursor();
    }
}
