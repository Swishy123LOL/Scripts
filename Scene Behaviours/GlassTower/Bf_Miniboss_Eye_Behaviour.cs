using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bf_Miniboss_Eye_Behaviour : MonoBehaviour
{
    public float distance;
    public float speed;
    public float _offset;
    public float bulletCount1 = 3;
    public float bulletCount2 = 2;
    public GameObject bullet;
    public AnimationCreator anim;
    public AnimationCreator anim2;
    public AnimationCreator anim3;
    public AnimationCreator anim4;
    SpriteRenderer ren;
    AudioManager audioManager;
    PlayerMovement m_player;
    Player player;
    Vector2 des;
    Vector2 lookDirection;
    float offset;
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
        dis = Vector2.Distance(transform.position, player.transform.position); 
        offset = Mathf.Sin(Time.time * 2) / 1.5f;

        if (((Vector2)transform.position - des).magnitude > .05f){            //This help stopping jittering movement
            transform.Translate(new Vector2(lookDirection.x, lookDirection.y + offset) * Time.fixedDeltaTime * 2);
        }

        if (((Vector2)transform.position - des).magnitude < .05f){
            transform.position = new Vector2(transform.position.x + .06f, transform.position.y);
        }

        if (dis < distance){
            if (scatter == false){
                scatter = true;
            }
        }

        if (dis > distance){
            facing = player.transform.position.x < transform.position.x;
            scatter = false;

            if (scatter == false){
                float step = 2*Time.fixedDeltaTime;
                des = new Vector2(player.transform.position.x, player.transform.position.y + 5);

                lookDirection = (des - (Vector2)transform.position).normalized;
            }
        }
    }

    IEnumerator Shoot(){
        int r = 0;
        int cr = 0;
        float i = 0;
        float step = Mathf.PI / 5;

        r = Random.Range(5, 15);
        while (true)
        {
            if (cr >= r){
                anim.StopAnimation();
                anim4.PlayAnimation();

                for (int o = 0; o < bulletCount2; o++)
                {
                    float mul = (transform.position.x < player.transform.position.x)? 1 : -1; 
                    BulletStat stat = Instantiate(bullet, transform.position , Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<BulletStat>();
                    audioManager.Play("Pew");

                    stat.p = new float[4];
                    stat.direction = 1;
                    cr = 0;
                    r  = Random.Range(5, 15);

                    float angle = Mathf.Deg2Rad * Vector2.Angle(transform.position - player.transform.position, Vector2.left);
                    angle *= (transform.position.y > player.transform.position.y) ? 1 : -1;

                    stat.p[0] = angle;
                    stat.p[1] = 3*speed;
                    stat.p[2] = 3;
                    stat.p[3] = 6;

                    yield return new WaitForSeconds(.15f);
                }

                anim4.StopAnimation();
                anim.PlayAnimation();
            }

            else
            {
                audioManager.Play("Pew");
                for (int o = 0; o < bulletCount1; o++)
                {
                    float mul = (transform.position.x < player.transform.position.x)? -1 : 1; 
                    BulletStat stat = Instantiate(bullet, transform.position , Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<BulletStat>();

                    stat.p = new float[4];
                    stat.direction = 1;

                    cr++;
                    stat.p[0] = step * (o+1) +  _offset * mul; 
                    stat.p[1] = speed;
                    stat.p[2] = 1;
                    stat.p[3] = 1;

                    yield return new WaitForSeconds(.1f);
                }    
            }
            
            yield return new WaitForSeconds(3f);
            if (i == 2){
                yield return new WaitForSeconds(4f);    
                i = 0;        
            }
            i++;
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
