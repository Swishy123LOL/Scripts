using System.Collections;
using UnityEngine;

public class E_PhaserBehaviour : MonoBehaviour
{
    public GameObject bullet;
    AnimationController anim;
    SpriteRenderer ren;
    EnemyCollision col;
    EnemyAura aura;
    Player player;

    void Start()
    {
        anim = GetComponent<AnimationController>();
        ren = GetComponent<SpriteRenderer>();
        col = GetComponent<EnemyCollision>();
        player = FindObjectOfType<Player>();
        aura = GetComponent<EnemyAura>();
        StartCoroutine(Shoot());

        anim.animations[1].OnEndAnimation += () =>
        {
            anim.currentIndex = 2;

            BulletStat stat = Instantiate(bullet, transform.position, Quaternion.identity, GameObject.Find("BulletContainer").transform).GetComponent<BulletStat>();

            float angle = Mathf.Deg2Rad * Vector2.Angle(transform.position - player.transform.position, Vector2.left);
            angle *= (transform.position.y > player.transform.position.y) ? -1 : 1;

            stat.p[0] = angle;
            stat.p[1] = Random.Range(1f, 2f);
        };

        anim.animations[2].OnEndAnimation += () =>
        {
            anim.currentIndex = 0;
            aura.Change(EnemyAura.AuraState.White);

            col.rbCollider.enabled = false;
            col.vulnerable = true;
        };
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));

            aura.Change(EnemyAura.AuraState.Red);
            col.StopAllCoroutines();
            ren.material = col.normal;
            anim.currentIndex = 1;
        }
    }
}
