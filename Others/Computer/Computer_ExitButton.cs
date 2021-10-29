using UnityEngine;
using UnityEngine.UI;

public class Computer_ExitButton : MonoBehaviour
{
    public Button ExitButton;

    void Start() {
        Computer_WindowBehaviour window = FindObjectOfType<Computer_WindowBehaviour>();

        ExitButton.onClick = window.OnWindowExit;
    }
}
