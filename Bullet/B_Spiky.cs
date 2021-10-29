using UnityEngine;

public class B_Spiky : MonoBehaviour
{
    Rigidbody2D rb;
    BulletStat stat;

    void Awake(){
        stat = GetComponent<BulletStat>();
    }
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(0 , stat.p[0]), 2 * Random.Range(1, 3)), ForceMode2D.Impulse);
    }
}
