using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScence1 : MonoBehaviour
{
    public GameObject GameObject;
    public GameObject player;
    public static bool Bool;
    public CinemachineVirtualCamera cam;

    void Start ()
    {
        player.SetActive(false);
    }
    public void Trigger ()
    {
        Bool = true;
        player.SetActive(true);
        cam.Priority = 0;
        GameObject.SetActive(false);
    }
}
