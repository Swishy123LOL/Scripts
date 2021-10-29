using UnityEngine;

public class B_BigCircle : MonoBehaviour
{
    BulletStat stat;
    void Start()
    {
        stat = GetComponent<BulletStat>();
    }

    void Update()
    {
        transform.position += new Vector3(Mathf.Cos(stat.p[0]) * stat.p[1] * Time.deltaTime, Mathf.Sin(stat.p[0]) * stat.p[1] * Time.deltaTime + Mathf.Sin(Time.time * 20) / 60); //Handle direction
    }
}
