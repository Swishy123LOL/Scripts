using UnityEngine;
using System.Collections;

public class B_Bump : MonoBehaviour
{
    public GameObject prefab; //p[0]: offset; p[1]: delay; p[2]: count 
    GameObject[] enemies;
    BulletStat stat;
    Vector3 pos;
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        stat = GetComponent<BulletStat>();
        pos = transform.position;
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < stat.p[2]; i++)
        {
            GameObject obj = Instantiate(prefab, new Vector3(pos.x + stat.p[0] * i * stat.direction, pos.y, pos.z), Quaternion.identity, GameObject.Find("BulletContainer").transform);
            Collider2D[] cols = obj.GetComponentsInChildren<Collider2D>();

            for (int j = 0; j < enemies.Length; j++)
            {
                Collider2D[] ecols = enemies[j].GetComponents<Collider2D>();
                for (int u = 0; u < ecols.Length; u++)
                {
                    Physics2D.IgnoreCollision(ecols[u], cols[0]);
                    Physics2D.IgnoreCollision(ecols[u], cols[1]);
                }
            }

            SpriteRenderer ren = obj.GetComponent<SpriteRenderer>();
            BulletStat _stat = ren.GetComponentInChildren<BulletStat>();

            ren.flipX = (stat.direction == 1)? true : false;
            _stat = stat;
            yield return new WaitForSeconds(stat.p[1]);
        }

        Destroy(gameObject);
    }
}
