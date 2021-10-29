using UnityEngine;

public class Computer_ChatHandler : MonoBehaviour, IOnWindowExit
{
    public int Messages;
    public bool Choice;
    public Computer_ChatBehaviour.ChatType type = Computer_ChatBehaviour.ChatType.Normal;
    public void OnWindowExit()
    {
        Computer_ChatBehaviour Chat = FindObjectOfType<Computer_ChatBehaviour>();
        Messages = Chat.CurrentMessages;
    }
}
