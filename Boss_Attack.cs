using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
    public Transform Boss;
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Boss.position = new Vector2(GetComponent<Boss>().newStartpos.position.x, Boss.transform.position.y);
    }
}
