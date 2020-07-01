using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedArea : MonoBehaviour
{
    public static bool Ignored = false;
    public GameObject SpawnPoint;
    public GameObject Player;
    public Animator Animator;
    public Collider2D Collider2D;
   void OnTriggerEnter2D(Collider2D collider2D)
    {
        Ignored = true;
        Animator.SetBool("Bool", true);
    }
    
    void Start ()
    {
        Collider2D.enabled = false;
    }
    void Update ()
    {
        if (DialogueManager.moveToSpawn == true)
        {
            Animator.SetBool("Bool", false);
            Player.transform.position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z);
            DialogueManager.moveToSpawn = false;
            Ignored = false;
            Collider2D.enabled = true;
        }
    }
}
