using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(EnemyAI))]

public class E_DummyBehaviour : MonoBehaviour
{
    public GameObject bullet;
    public float speed;
    public float count;
    public float offset;
    public float delay;
    public float randomness;
    public int min;
    public int max;
    public int a;
    public bool pause;

    EnemyAura aura;
    EnemyAI ai;

    void Start(){
        aura = GetComponent<EnemyAura>();
        ai = GetComponent<EnemyAI>();
        StartCoroutine(Shoot());

        FindObjectOfType<PlayerCollision>().OnHit += delegate { a = 0; };
    }

    public void _Shoot()
    {
        ai = GetComponent<EnemyAI>();
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot(){
        float i = 0;
        float step = Mathf.PI / (count+1);
        int r = UnityEngine.Random.Range(min, max);
        while (true)
        {
            if (!pause)
            {
                for (int o = 0; o < count; o++)
                {
                    BulletStat stat = Instantiate(bullet, transform.position, Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<BulletStat>();
                    stat.direction = (ai.flip) ? 1 : -1;
                    a++;

                    stat.p = new float[3];
                    stat.p[0] = step * (o + 1) + Mathf.PI / 2 + i * (offset / 10);
                    stat.p[1] = speed;
                    stat.p[2] = randomness;
                }

                yield return new WaitForSeconds(.1f);
                if (i == r)
                {
                    yield return new WaitForSeconds(delay);
                    i = 0;
                    r = UnityEngine.Random.Range(min, max);
                }
                i++;
            }
            yield return null;
        }
    }
}