using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;


    void OnTriggerEnter2D(Collider2D collider)
    {
        cam1.Priority = 5;
        cam2.Priority = 10;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        cam1.Priority = 10;
        cam2.Priority = 5;
    }

}
