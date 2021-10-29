using UnityEngine;

public class B_Tornado : MonoBehaviour
{
    BulletStat stat; //p[0]: speed p[1]: curvature
    Vector3 player;
    Vector3 trans;
    Vector3 mid;
    float delta;
    float t;
    bool a;

    void Start()
    {
        player = FindObjectOfType<Player>().transform.position;
        stat = GetComponent<BulletStat>();
        trans = transform.position;

        float dis = Vector3.Distance(transform.position, player);
        if (dis > 1) stat.p[0] /= dis;

        t = Random.Range(-1f, 1f);
        mid = transform.position - (transform.position - player) / 2;
        mid.y += Random.Range(-stat.p[1] * dis, stat.p[1] * dis);
        t = 0;

    }

    void Update()
    {
        t += Time.deltaTime * stat.p[0];
        if (t <= 1)
            transform.position = path(trans, mid, player, t);

        if (t > 1)
        {
            if (!a)
            {
                delta = Time.deltaTime;
                a = true;
            }

            transform.position += (path(trans, mid, player, 1) - path(trans, mid, player, 1 - delta)) * stat.p[0];
        }     
    }

    Vector3 path(Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return (1 - t) * (1 - t) * p1 + 2 * (1 - t) * t * p2 + t * t * p3;
    }
}
