using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition_TP : MonoBehaviour
{
    public CinemachineVirtualCamera n_cam;
    public CinemachineVirtualCamera p_cam;

    public static bool isPlaying = false;
    bool debool = false;

    public GameObject Spawn;
    public GameObject[] Players;
    public Animator Animator;
    public Animator PlayerAnimator;

    void Start()
    {
        PlayerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        if (debool == true && PlayerAnimator.GetBool("IsBack") == true)
        {
            StartCoroutine(wait());
            debool = false; 
        }
    }
    void OnTriggerEnter2D (Collider2D collider2D)
    {
        debool = true;

        if (collider2D.gameObject.CompareTag("Player") && PlayerAnimator.GetBool("IsBack") == true)
        {
            StartCoroutine(wait());
        }
    }

    void OnTriggerExit2D (Collider2D collider2D)
    {
        debool = false;
    }

    IEnumerator wait ()
    {
        Animator.SetBool("Bool", true);

        isPlaying = true;

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject Player in Players)
        {
            Player.transform.position = new Vector3(Spawn.transform.position.x, Spawn.transform.position.y, Player.transform.position.z);
        }


        n_cam.Priority = 5;
        p_cam.Priority = 10;

        yield return new WaitForSeconds(36f/40);

        isPlaying = false;

        Animator.SetBool("Bool", false);
    }

}