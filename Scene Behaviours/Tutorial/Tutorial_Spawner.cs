using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Spawner : MonoBehaviour
{
    public GameObject laser;
    public Transform pos;
    
    void Start()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        while (true)
        {
            GameObject _laser = Instantiate(laser, new Vector3(pos.position.x + Random.Range(1, 10) , pos.position.y), Quaternion.identity, transform);
            LeanTween.moveY(_laser, _laser.transform.position.y - Random.Range(2, 4), 2).setEaseOutSine();
            yield return new WaitForSeconds(Random.Range(4f, 10f));
        }
    }
}
