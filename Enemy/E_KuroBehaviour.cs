using System.Collections;
using UnityEngine;

public class E_KuroBehaviour : MonoBehaviour
{
    [Header("Properties")]
    public AnimationCreator anim;
    public AnimationCreator anim2;
    public AnimationCreator anim3;
    public AnimationCreator anim4;
    public SpriteRenderer[] sprites;
    public Sprite[] hurtSprites;
    public GameObject bullet;

    [Header("Stat")]
    public float speedMin;
    public float speedMax;
    public float curvatureMin;
    public float curvatureMax;
    public float attackChance;
    public int bulletCountMin;
    public int bulletCountMax;

    Sprite[] defSprites;
    Coroutine coroutine;
    EnemyCollision col;
    Animator animator;
    EnemyAura aura;
    EnemyAI ai;
    bool a, b;

    void Start()
    {
        col = GetComponent<EnemyCollision>();
        animator = GetComponent<Animator>();
        aura = GetComponent<EnemyAura>();
        ai = GetComponent<EnemyAI>();
        StartCoroutine(Move());

        defSprites = new Sprite[3];
        for (int i = 0; i < 3; i++)
        {
            defSprites[i] = sprites[i].sprite;
        }

        anim.OnEndAnimation += () =>
        {
            ai.forceStop = true;
            transform.position += new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));
            anim2.PlayAnimation();
        };

        anim2.OnEndAnimation += () =>
        {
            bool b = HelpingHand.RandomChanceFloat(100, attackChance);
            sprites[3].sprite = null;

            for (int i = 0; i < 3; i++)
            {
                sprites[i].sprite = defSprites[i];
            }

            if (!b)
            {
                aura.Change(EnemyAura.AuraState.White);
                animator.SetBool("a", false);
                ai.forceStop = false;
            }

            else if (b)
            {
                aura.Change(EnemyAura.AuraState.Red);
                if (coroutine != null)
                    StopCoroutine(coroutine);

                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.sprite = null;
                }

                anim3.PlayAnimation();
            }
        };

        anim3.OnEndAnimation += () =>
        {
            for (int i = 0; i < Random.Range(bulletCountMin, bulletCountMax + 1); i++)
            {
                BulletStat stat = Instantiate(bullet, transform.position, Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<BulletStat>();
                stat.p[0] = Random.Range(speedMin, speedMax);
                stat.p[1] = Random.Range(curvatureMin, curvatureMax);
            }

            anim4.PlayAnimation();
        };

        anim4.OnEndAnimation += () =>
        {
            sprites[3].sprite = null;
            for (int i = 0; i < 3; i++)
            {
                sprites[i].sprite = defSprites[i];
            }

            ai.forceStop = false;
            animator.SetBool("a", false);
            aura.Change(EnemyAura.AuraState.White);
        };

        col.onTakeDamage += () =>
        {
            if (coroutine != null) 
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(TakeDamage());
        };
    }

    IEnumerator TakeDamage()
    {
        for (int i = 0; i < 3; i++)
        {
            sprites[i].sprite = hurtSprites[i];
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < 3; i++)
        {
            sprites[i].sprite = defSprites[i];
        }
    }

    void Update()
    {
        if (a)
        {
            a = false;
            aura.Change(EnemyAura.AuraState.Red);
            if (coroutine != null)
                StopCoroutine(coroutine);

            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.sprite = null;
            }

            animator.SetBool("a", true);

            anim.PlayAnimation();
            ai.forceStop = true;
        }
    }

    public void SetBool()
    {
        if (b)
        {
            a = true;
            b = false;
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(6, 8));
            b = true; //Actual code is in Update()
        }
    }
}
