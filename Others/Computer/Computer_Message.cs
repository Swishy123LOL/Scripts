using UnityEngine;

[System.Serializable]
public class Computer_Message
{
    public Sprite MessageSprite;
    public float time;
    [TextArea(1, 3)]
    public string Message;
    [Space]
    public Type type;
    public enum Type
    {
        Default,
        Option,
        Corrupted
    }
}
