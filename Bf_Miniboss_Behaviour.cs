using System.Collections;
using UnityEngine;
using Cinemachine;

public class Bf_Miniboss_Behaviour : MonoBehaviour
{
    [Header("Phases")]
    public Phase phase;
    public HealthBar_Boss hbar;
    public EnemyStat stat;
    bool first;
    bool second;
    bool final;
    float hp;
    public GameObject eye1;
    public GameObject eye2;
    public GameObject dummy;
    public SpriteRenderer[] warning;
    public TriggerTimeline timeline;
    public CinemachineVirtualCamera cam;
    public BoxCollider2D[] _colliders;
    GameObject _eye1;
    GameObject _eye2;
    AnimationCreator anim;
    AudioManager audioManager;
    public enum Phase {
        phase1,
        phase2,
        phase3
    };
    EnemyCollision ecol;
    PlayerAttack attack;
    SpriteRenderer ren;
    Material material;   
    Player player;
    [Header("1st Attack")]
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Sprite defSprite;
    public AnimationCreator anim2;
    public AnimationCreator anim3;
    [Header("2nd Attack")]
    public Transform flyPos;
    public Transform landPos;
    public GameObject bullet;
    public GameObject bullet2;
    public AnimationCurve fallingCurve;
    Vector2 lookDirection;
    bool vulnerable;
    bool fall;
    float time;
    float x;
    float y;
    int n;
    void Start(){
        audioManager = FindObjectOfType<AudioManager>();
        attack = FindObjectOfType<PlayerAttack>();
        anim = GetComponent<AnimationCreator>();
        ecol = GetComponent<EnemyCollision>();
        ren = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();

        material = ren.material;

        first = true;
        hp = stat.Health;
        ecol.onTakeDamage += TakeDamage;
        ecol.onNoHealth += OnNoHealth;
        StartCoroutine(Attack1());

        audioManager.Play("MiniBattle");

        Physics2D.IgnoreCollision(_colliders[0], GameObject.Find("Enviroment").GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(_colliders[1], GameObject.Find("Enviroment").GetComponent<BoxCollider2D>());
    }

    void TakeDamage(){
        hbar.TakeDamage(attack.lastDamage);
    }

    void Update(){
        if (stat.Health <= hp / 2) final = true;

        if (phase == (Phase)2){
            Vector3 des = new Vector3(player.transform.position.x, transform.position.y);
            float offset = Mathf.Sin(Time.time * 1.5f) / 2;

            if (Mathf.Abs(transform.position.x - player.transform.position.x) > 0.1f){
                ren.flipX = player.transform.position.x >= transform.position.x;   
                lookDirection = (des - transform.position).normalized;
            }

            transform.Translate(new Vector2(lookDirection.x, lookDirection.y + offset) * Time.fixedDeltaTime * 2);

            if (fall == true){
                time += Time.deltaTime/2;
                transform.position = new Vector2(x + time, y + 3*fallingCurve.Evaluate(time));

                if (time >= fallingCurve.keys[fallingCurve.length-1].time) {
                    n = 0;
                    fall = false;
                    phase = (Phase)1;

                    anim.StopAnimation();
                    anim3.PlayAnimation();

                    StopAllCoroutines();
                    StartCoroutine(Attack1());
                }    
            }
        }
    }

    void OnNoHealth(){
        //ded
        GameObject obj = Instantiate(dummy, transform.position, Quaternion.identity);
        obj.name = "Cube_Boss_Dummy";
        Destroy(GameObject.Find("CM vcam2"));
        Destroy(GameObject.Find("Bounder2"));

        timeline.PlayTimeline();

        _eye1?.GetComponent<Bf_Miniboss_Eye_Behaviour>().SelfDestroy();
        _eye2?.GetComponent<Bf_Miniboss_Eye2_Behaviour>().SelfDestroy();

        FindObjectOfType<BulletContainer>().RemoveAll();

        Destroy(hbar.gameObject);

        Destroy(gameObject);
    }

    IEnumerator Attack2(){
        int r = Random.Range(2, 5);
        int i = 0;
        int e = 0;

        if (first) audioManager.Pause("MiniBattle");

        cam.Follow = transform;

        yield return new  WaitForSeconds(1);

        ren.enabled = false;
        anim2.GetComponent<SpriteRenderer>().flipX = ren.flipX;
        anim2.PlayAnimation();

        audioManager.Play("Hit");

        yield return new WaitForSeconds(1);

        ren.enabled = true;
        anim.PlayAnimation();
        anim2.GetComponent<SpriteRenderer>().sprite = null;

        if (first) audioManager.Resume("MiniBattle");
        audioManager.Play("Engine");

        LeanTween.moveY(gameObject, flyPos.position.y, 2).setEaseInOutSine();

        yield return new WaitForSeconds(2);

        cam.Follow = player.transform;

        vulnerable = true;


        while (true)
        {
            if (i >= r){
                i = 0;
                r = Random.Range(2, 5);

                for (int o = 0; o < 3; o++)
                {
                    if (!first){
                        BulletStat stat = Instantiate(bullet, transform.position , Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<BulletStat>();

                        stat.p = new float[1];
                        stat.p[0] = (player.transform.position.x > transform.position.x)? 6 : -6;
                    }
                    yield return new WaitForSeconds(0.05f);
                }
            }

            if (e == 8){
                LeanTween.moveY(gameObject, landPos.position.y, 2).setEaseInOutSine();
                yield return new WaitForSeconds(2);

                phase = (Phase)1;
                second = true;
                first = false;
                n = 0;

                if (second && !final) {
                    if (_eye1 != null) _eye1?.GetComponent<Bf_Miniboss_Eye_Behaviour>().SelfDestroy();
                }

                anim.StopAnimation();
                anim3.PlayAnimation();

                StartCoroutine(Attack1());  

                break;  
            }

            e++;
            i++;
            yield return new WaitForSeconds(2);
        }
    }                                                  

    IEnumerator Attack1(){
        int r = (first)? 3 : Random.Range(2, 5);
        int i = 0;
        bool d = player.transform.position.x < transform.position.x;
        Transform pos = d? pos3 : pos4;

        audioManager.Stop("Engine");     

        if (second && !final) {
            _eye2?.GetComponent<Bf_Miniboss_Eye2_Behaviour>().SelfDestroy();
            _eye1 = Instantiate(eye1, pos.position, Quaternion.identity);
        }

        if (final){
            if (_eye1 == null) _eye1 = Instantiate(eye1, pos3.position, Quaternion.identity);

            Bf_Miniboss_Eye_Behaviour behaviour1 = _eye1.GetComponent<Bf_Miniboss_Eye_Behaviour>();
            behaviour1.bulletCount1 = 2;
            behaviour1.bulletCount2 = 1;

            if (_eye2 == null) _eye2 = Instantiate(eye2, pos4.position, Quaternion.identity);

            Bf_Miniboss_Eye2_Behaviour behaviour2 = _eye2.GetComponent<Bf_Miniboss_Eye2_Behaviour>();
            behaviour2.bulletCount = 1;
            behaviour2.waitTime = 5;
        }

        while (true)
        {
            material.SetColor("_OutCol", Color.red);
            ecol.forceStop = true;
            gameObject.layer = 13;

            LeanTween.moveLocalY(gameObject, player.transform.localPosition.y + Random.Range(0, 1), 1.5f).setEaseInOutSine();

            yield return new WaitForSeconds(1.5f);

            warning[0].transform.position = new Vector3(warning[0].transform.position.x, transform.position.y);
            warning[1].transform.position = new Vector3(warning[1].transform.position.x, transform.position.y);

            d = player.transform.position.x < transform.position.x;
            if (d) warning[0].enabled = true;
            else if (!d) warning[1].enabled = true;

            audioManager.Play("Bleep");

            Transform _pos1 = (player.transform.position.x < transform.position.x)? pos1 : pos2;
            Transform _pos2 = (player.transform.position.x < transform.position.x)? pos2 : pos1;
            int mul = (player.transform.position.x < transform.position.x)? 3 : -7;

            LeanTween.moveLocalX(gameObject, _pos1.position.x, 2).setEaseInBack();
            ren.flipX = player.transform.position.x > transform.position.x;
            cam.Follow = null; //Lock camera

            yield return new WaitForSeconds(2);

            GetComponent<SpriteRenderer>().enabled = false;
            warning[0].enabled = false;
            warning[1].enabled = false;

            cam.Follow = player.transform; //Unlock camera, follow player again

            yield return new WaitForSeconds(.5f);
            GetComponent<SpriteRenderer>().enabled = true;

            transform.position = new Vector2(_pos2.position.x, transform.position.y);
            LeanTween.moveLocalX(gameObject, _pos2.position.x - mul, 2).setEaseOutSine();

            i++;
            if (i == r) {
                pos = (player.transform.position.x < transform.position.x)? pos3 : pos4;
                if (second && !final){
                    _eye1?.GetComponent<Bf_Miniboss_Eye_Behaviour>().SelfDestroy();
                    _eye2 = Instantiate(eye2, pos.position, Quaternion.identity);
                }
                StartCoroutine(Attack2());

                if (first) {
                    yield return new WaitForSeconds(3);
                    _eye1 = Instantiate(eye1, pos.position, Quaternion.identity);
                }

                phase = (Phase)2;
                time = 0;
                n = 0;

                break;
            }

            material.SetColor("_OutCol", Color.white);
            ecol.forceStop = false;
            gameObject.layer = 9;
            
            yield return new WaitForSeconds(2 * Random.Range(1f, 3f));
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "bf_laser" && fall == false && phase == (Phase)2 && vulnerable == true){
            x = transform.position.x;
            y = transform.position.y;

            ecol.forceStop = false;

            if (stat.Health > 12)
            {
                hbar.TakeDamage(12); //This one is for displaying the health bar
                ecol.TakeDamage(12, false, false, false); //This one is for displaying the animation
            }

            else if (stat.Health <= 12)
            {
                hbar.TakeDamage(stat.Health - 1); //This one is for displaying the health bar
                ecol.TakeDamage(stat.Health - 1, false, false, false); //This one is for displaying the animation
            }

            ecol.forceStop = true;

            n++;
            if (n == 6){
                fall = true;
                vulnerable = false;
            }
        }
    }
}
