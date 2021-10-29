using System.Collections;
using UnityEngine;

public class B_ThrowThing : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject shadow;
    public GameObject mark;
    public Sprite[] sprites;
    public bool aimToPlayer;
    public float duration;
    public int i;
    public Vector2 force;
    AnimationCurve curve;
    SpriteRenderer ren2;
    SpriteRenderer ren;
    BoxCollider2D col;
    GameObject _mark;
    BulletStat bstat;
    Player player;
    Vector3 inital;
    float h;
    float r;
    float t;
    float s;

    void Start()
    {
        player = FindObjectOfType<Player>();
        r = UnityEngine.Random.Range(-force.y / 2, force.y / 2);

        inital = transform.position;
        curve = new AnimationCurve();
        curve.AddKey(0, 0);

        ren = GetComponent<SpriteRenderer>();
        ren.sprite = sprites[UnityEngine.Random.Range(0, 5)];
        s = ren.sprite.rect.width / 16 * transform.localScale.x; //NOTE: Sprite MUST be a square!

        Transform _shadow = Instantiate(shadow, GameObject.Find("BulletContainer").transform).transform;
        _shadow.localScale = new Vector2(s / 1.5f, s / 1.5f);
        _shadow.position = new Vector3(inital.x, inital.y, inital.z);

        if (!aimToPlayer)
        {
            curve.AddKey(duration / 2, force.y + r);
            curve.AddKey(duration, r);
            LeanTween.moveX(gameObject, inital.x + force.x, duration);

            _mark = Instantiate(mark, new Vector3(inital.x + force.x, inital.y + r, inital.z), Quaternion.identity, GameObject.Find("BulletContainer").transform);
            LeanTween.moveY(_shadow.gameObject, inital.y + r, duration);
        }

        else if (aimToPlayer)
        {
            float x = player.transform.position.x - transform.position.x;
            float y = player.transform.position.y - transform.position.y;

            curve.AddKey(duration / 2, Mathf.Abs(x) * 2);
            curve.AddKey(duration, y);
            LeanTween.moveX(gameObject, inital.x + x, duration);

            _mark = Instantiate(mark, new Vector3(inital.x + x, inital.y + y, inital.z), Quaternion.identity, GameObject.Find("BulletContainer").transform);
            LeanTween.moveY(_shadow.gameObject, inital.y + y, duration);
        }

        LeanTween.rotateZ(gameObject, 360 * duration * 8 / s, duration);
        _mark.transform.localScale = new Vector2(s / 1.3125f, s / 1.3125f);
        shadow = _shadow.gameObject;

        ren2 = _shadow.GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        bstat = GetComponent<BulletStat>();

        bstat.onCollidePlayer += () =>
        {
            Destroy(_mark);
            Destroy(shadow);
        };
    }

    void FixedUpdate()
    {
        t += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, inital.y + curve.Evaluate(t), inital.z);
        if (shadow)
            shadow.transform.position = new Vector3(transform.position.x, inital.y, inital.z);
        
        h = Mathf.Abs(transform.position.y - inital.y + r);
        if (ren2)
            ren2.color = new Color(1, 1, 1, 1 - (transform.position.y - inital.y) / 10 / transform.localScale.y);

        if (col)
        {
            if (transform.position.y - inital.y > 2)
                col.enabled = false;

            else if (transform.position.y - inital.y <= 2)
                col.enabled = true;
        }

        if (t >= duration)
        {
            ParticleSystem _particle = Instantiate(particle.gameObject, transform.position, Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<ParticleSystem>();
            _particle.Play();

            if (i != 0)
            {
                B_ThrowThing stat1 = Instantiate(gameObject, GameObject.Find("BulletContainer").transform).GetComponent<B_ThrowThing>();
                stat1.i--;
                stat1.force.x = force.x + UnityEngine.Random.Range(-force.x / (i * 2), force.x / (i * 2));
                stat1.force.y = force.y - force.y / (i * 2);
                stat1.duration += UnityEngine.Random.Range(-duration / 5, duration / 5);
                stat1.GetComponent<Transform>().localScale /= Mathf.Sqrt(2);
                stat1.aimToPlayer = false;

                B_ThrowThing stat2 = Instantiate(gameObject, GameObject.Find("BulletContainer").transform).GetComponent<B_ThrowThing>();
                stat2.i--;
                stat2.force.x = -force.x + UnityEngine.Random.Range(-force.x / (i * 2), force.x / (i * 2));
                stat2.force.y = force.y - force.y / (i * 2);
                stat2.duration += UnityEngine.Random.Range(-duration / 5, duration / 5);
                stat2.GetComponent<Transform>().localScale /= Mathf.Sqrt(2);
                stat2.aimToPlayer = false;
            }

            Destroy(_mark);
            Destroy(shadow);
            Destroy(gameObject);
        }
    }
}
