using UnityEngine;
using UnityEngine.UI;

public class Computer_Corrupted_Choice : MonoBehaviour
{
    public Button YesButton;
    public Button NoButton;
    public void TransitionYes() {
        FindObjectOfType<Computer_Corrupted>().Transition();

        Computer_ChatHandler chat = FindObjectOfType<Computer_ChatHandler>();
        chat.type = Computer_ChatBehaviour.ChatType.Corrupted;  
        chat.Choice = true;
        chat.Messages = 0;

        FindObjectOfType<Computer_WindowBehaviour>().Playsound("Ambience");
    }

    public void TransitionNo() {
        FindObjectOfType<Computer_Corrupted>().Transition();

        Computer_ChatHandler chat = FindObjectOfType<Computer_ChatHandler>();
        chat.type = Computer_ChatBehaviour.ChatType.Corrupted;  
        chat.Choice = false;
        chat.Messages = 0;

        FindObjectOfType<Computer_WindowBehaviour>().Playsound("Ambience");
    }
}
