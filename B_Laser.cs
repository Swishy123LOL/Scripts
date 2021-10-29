using UnityEngine;
using System.Collections;
using System;
public class B_Laser : MonoBehaviour
{
    public AnimationCreator anim1, anim2;
    public bool childObject;
    public float waitTime = 1f; 
    BulletStat stat;
    public void Start(){
        if (!childObject) {
            stat = GetComponent<BulletStat>();
            transform.Rotate(new Vector3(0, 0, stat.p[0]));     
        }

        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation(){
        yield return new WaitForSeconds(waitTime);

        GetComponent<BoxCollider2D>().enabled = true;
        anim2.PlayAnimation();

        FindObjectOfType<AudioManager>().Play("Pew2");

        anim2.OnEndAnimation += Destroy;

        yield return new WaitForSeconds(.1f);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void Destroy(){
        Destroy(gameObject);
    }
}
