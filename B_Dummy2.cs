using UnityEngine;

public class B_Dummy2 : MonoBehaviour
{
    BulletStat stat;
    void Start(){
        stat = GetComponent<BulletStat>();
    }
    
    void Update(){
        transform.position += new Vector3(Mathf.Cos(stat.p[0]) * Time.deltaTime * -stat.p[1] * stat.direction, 
        Mathf.Sin(stat.p[0]) * Time.deltaTime * -stat.p[1] * stat.direction + Mathf.Sin(Time.time * stat.p[1]) / 20); //Handle direction
    }
}
