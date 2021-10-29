using UnityEngine;
using System.Collections;

public class E_HuskBehaviour : MonoBehaviour
{
    AnimationCreator firstAnim, secondAnim, hoverAnim;
    public GameObject bullet;
    public GameObject bullet2;
    public float delayMin, delayMax;
    public float countMin, countMax;
    public float hoverTime;
    public AnimationCreator[] anims;
    CharacterMoveAnimation charMove;
    EnemyAura aura;
    Player player;
    EnemyAI ai;
    int dir = 1;
    bool isHit;

    void Start()
    {
        ai = GetComponent<EnemyAI>();
        aura = GetComponent<EnemyAura>();
        player = FindObjectOfType<Player>();
        charMove = GetComponent<CharacterMoveAnimation>();

        anims[0].OnEndAnimation += () =>
        {
            anims[1].PlayAnimation();
            StartCoroutine(Hover());
        };

        anims[2].OnEndAnimation += () =>
        {
            Vector3 p = transform.position;
            p.x += dir * 2;

            BulletStat stat = Instantiate(bullet, p, Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<BulletStat>();
            stat.direction = dir;
            stat.p[0] = .75f;
            stat.p[1] = Random.Range(delayMin, delayMax);
            stat.p[2] = Random.Range(countMin, countMax);

            anims[3].PlayAnimation();
        };

        anims[3].OnEndAnimation += () =>
        {
            aura.Change(EnemyAura.AuraState.White);
            charMove.forceStopForceStop = false;
            ai.forceStopForceStop = false;
        };

        anims[4].OnEndAnimation += () =>
        {
            anims[5].PlayAnimation();
            StartCoroutine(Hover());
        };

        anims[6].OnEndAnimation += () =>
        {
            Vector3 p = transform.position;
            p.x += dir * 2;

            BulletStat stat = Instantiate(bullet, p, Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<BulletStat>();
            stat.direction = dir;
            stat.p[0] = .75f;
            stat.p[1] = Random.Range(delayMin, delayMax);
            stat.p[2] = Random.Range(countMin, countMax);

            anims[7].PlayAnimation();
        };

        anims[7].OnEndAnimation += () =>
        {
            aura.Change(EnemyAura.AuraState.White);
            charMove.forceStopForceStop = false;
            ai.forceStopForceStop = false;
        };

        anims[8].OnEndAnimation += () =>
        {
            anims[9].PlayAnimation();
            B_ThrowThing stat = Instantiate(bullet2, transform.position, Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<B_ThrowThing>();
        };

        anims[9].OnEndAnimation += () =>
        {
            aura.Change(EnemyAura.AuraState.White);
            charMove.forceStopForceStop = false;
            ai.forceStopForceStop = false;
        };

        StartCoroutine(Shoot());
    }

    void Update()
    {
        Vector2 _dir = (transform.position.x < player.transform.position.x) ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 5), 0, _dir, Mathf.Infinity, 11);

        isHit = hit.transform != null;
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(8, 9));
            if (isHit && HelpingHand.RandomChanceInt(2))
            {
                firstAnim = (transform.position.x > player.transform.position.x) ? anims[0] : anims[4];
                hoverAnim = (transform.position.x > player.transform.position.x) ? anims[1] : anims[5];
                secondAnim = (transform.position.x > player.transform.position.x) ? anims[2] : anims[6];
                dir = (transform.position.x > player.transform.position.x) ? -1 : 1;

                charMove.forceStopForceStop = true;
                ai.forceStopForceStop = true;
                aura.Change(EnemyAura.AuraState.Red);

                firstAnim.PlayAnimation();
            }

            else
            {
                charMove.forceStopForceStop = true;
                ai.forceStopForceStop = true;
                aura.Change(EnemyAura.AuraState.Red);

                anims[8].PlayAnimation();
            }
        }
    }

    IEnumerator Hover()
    {
        yield return new WaitForSeconds(hoverTime);
        hoverAnim.StopAnimation();
        secondAnim.PlayAnimation();
    }
}
