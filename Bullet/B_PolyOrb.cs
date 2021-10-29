using System.Collections;
using UnityEngine;

public class B_PolyOrb : MonoBehaviour
{
    public GameObject laser;
    public Transform[] pos;

    void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        GameObject[] obj = new GameObject[4];
        BulletStat[] stats = new BulletStat[4];
        LeanTween.rotateZ(gameObject, 720 + Random.Range(0f, 360f), 2).setEaseOutExpo().setEaseInExpo();

        for (int i = 0; i < 4; i++)
        {
            obj[i] = Instantiate(laser, pos[i].position, Quaternion.identity, transform);
            stats[i] = obj[i].GetComponent<BulletStat>();
            stats[i].p = new float[2];
            stats[i].p[1] = 1;

            B_Laser b_Laser = obj[i].GetComponent<B_Laser>();
            b_Laser.waitTime = 1.5f;

            obj[i].SetActive(false);
        }

        yield return new WaitForSeconds(2);

        for (int i = 0; i < 4; i++)
        {
            obj[i].SetActive(true);
            obj[i].GetComponent<B_Laser>().Start();
        }

        stats[0].p[0] = 0;
        stats[1].p[0] = 90;
        stats[2].p[0] = 180;
        stats[3].p[0] = -90;
    }
}
