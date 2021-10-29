using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bf_Miniboss_Eye2_Behaviour : MonoBehaviour
{
    public float distance;
    public float speed;
    public float _offset;
    public float waitTime = 4;
    public float bulletCount = 2;
    public GameObject bullet;
    PlayerMovement m_player;
    Player player;
    Vector2 des;
    public AnimationCreator anim;
    public AnimationCreator anim2;
    public AnimationCreator anim3;
    SpriteRenderer ren;
    AudioManager audioManager;
    Vector2 lookDirection;
    float dis;
    bool scatter; 
    bool facing;
    void Start(){
        ren = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        m_player = FindObjectOfType<PlayerMovement>();
        audioManager = FindObjectOfType<AudioManager>();
        
        StartCoroutine(_Start());
    }

    IEnumerator _Start(){
        foreach (Collider2D cld in FindObjectsOfType<Collider2D>())
        {
            if (cld.transform.name == "BounderCollider") continue;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), cld);
        }

        ren.sprite = null;
        anim2.PlayAnimation();

        audioManager.Play("Appear");

        yield return new WaitForSeconds(2f);

        anim.PlayAnimation();
        Destroy(anim2.gameObject);


        des = new Vector2(player.transform.position.x, player.transform.position.y + 5);  
        lookDirection = (des - (Vector2)transform.position).normalized;
        
        StartCoroutine(Shoot());
    }
    void FixedUpdate(){
        //Reused codes
        dis = Mathf.Abs(transform.position.x - player.transform.position.x); 
        float offset = Mathf.Sin(Time.time * 2) / 1.5f;

        if (((Vector2)transform.position - des).magnitude > .05f){            //This help stopping jittering movement
            transform.Translate(new Vector2(lookDirection.x, lookDirection.y + offset) * Time.fixedDeltaTime * 3.5f);
        }

        if (((Vector2)transform.position - des).magnitude < .05f){
            transform.position = new Vector2(transform.position.x + .06f, transform.position.y);
        }

        if (dis < distance){
            if (scatter == false){
                scatter = true;
                des = new Vector2(des.x, des.y + 3);
            }
        }

        if (dis > distance){
            facing = player.transform.position.x < transform.position.x;
            scatter = false;

            if (scatter == false){
                float step = 2*Time.fixedDeltaTime;
                des = new Vector2(player.transform.position.x, player.transform.position.y + 6);

                lookDirection = (des - (Vector2)transform.position).normalized;
            }
        }
    }

    IEnumerator Shoot(){
        float i = 1;
        yield return new WaitForSeconds(4);

        while (true)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 2*distance){
                float mul = (transform.position.x < player.transform.position.x)? 1 : -1; 
                BulletStat stat = Instantiate(bullet, transform.position , Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<BulletStat>();

                stat.p = new float[1];
                stat.p[0] = Vector2.Angle(transform.position - player.transform.position, Vector2.right);
            
                yield return new WaitForSeconds(.1f);
                if (i == bulletCount){
                    yield return new WaitForSeconds(waitTime);    
                    i = 0;        
                }
                i++;
            }

            yield return null;
        }
    }

    public void SelfDestroy(){
        anim.StopAnimation();
        ren.sprite = null;
        anim3.PlayAnimation();

        anim3.OnEndAnimation += DestroySelf;
    }

    void DestroySelf(){
        Destroy(gameObject);
    }
}
