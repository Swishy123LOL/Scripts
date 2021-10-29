using UnityEngine;
[RequireComponent(typeof(BulletStat))]

public class B_Dummy : MonoBehaviour
{
    BulletStat stat;
    float time;
    void Start()
    {
        stat = GetComponent<BulletStat>();
    }

    void Update()
    {
        time += Time.deltaTime;
        transform.position += new Vector3(Mathf.Cos(stat.p[0]) * stat.p[1] * Time.deltaTime * Mathf.Exp(time * stat.p[2])* stat.direction, 
        Mathf.Sin(-stat.p[0]) * stat.p[1] * Time.deltaTime * Mathf.Exp(time * stat.p[2])* stat.direction) / stat.p[3];
    }
}
