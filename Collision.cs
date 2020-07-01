using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    SpriteRenderer Object;

    void Start()
    {
        Object = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Object.sortingOrder -= 10;
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        Object.sortingOrder += 10;
    }
    
}
