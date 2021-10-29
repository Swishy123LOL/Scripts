using UnityEngine;

public class FireflyAI : MonoBehaviour
{
    Vector2 a, b;
    Vector2 p;
    float t;
    public float dis;
    public float speed = 1f;
    bool changed;
    void Start(){
        a = transform.position; b = transform.position;
        a = new Vector2(a.x + Random.Range(-dis, dis), a.y + Random.Range(-dis, dis));
        b = new Vector2(b.x + Random.Range(-dis, dis), b.y + Random.Range(-dis, dis));
        speed += Random.Range(-speed/2, speed/2);
    }

    void Update(){
        t = (Mathf.Sin(Time.time * speed) + 1) / 2;

        p.x = (1-t)*(1-t)*a.x + t*t*b.x;  
        p.y = (1-t)*(1-t)*a.y + t*t*b.y; 

        transform.position = p;

        if (t<.01f && !changed){
            changed=true;
            b = new Vector2(b.x + Random.Range(-dis, dis), b.y + Random.Range(-dis, dis));
        } 
        if (t>.99f && changed) {
            changed=false;  
            a = new Vector2(a.x + Random.Range(-dis, dis), a.y + Random.Range(-dis, dis));
        }
    }
}
