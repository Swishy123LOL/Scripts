using TMPro;
using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyStat))]
public class EnemyCollision : MonoBehaviour
{
    [Header("ForceStop")]
    public bool forceStop;
    [Header("Properties")]
    public Material flash;
    public Sprite defaultSprite, hurtSprite, destroyedSprite;
    public GameObject damageText;
    public GameObject critDamageText;
    public GameObject blockedDamageText;
    public GameObject woundEffect;
    public Action onTakeDamage;
    public Action onNoHealth;
    public BoxCollider2D rbCollider;
    public BoxCollider2D colliderIgnore;

    [Space]
    public float yOffset;
    CharacterMoveAnimation moveAnim;
    AnimationController controlAnim;
    PlayerMovement move;
    SpriteRenderer ren;
    PlayerAttack pStat;
    [HideInInspector]
    public Material normal;
    Coroutine coroutine;
    EnemyStat stat;
    Rigidbody2D rb;
    Player player;
    EnemyAI ai;
    bool destroyed;
    [HideInInspector]
    public bool vulnerable = true;
    [Header("Stat")]
    public float knockbackRate = 1;
    public bool spin;
    void Start(){
        moveAnim = GetComponent<CharacterMoveAnimation>();
        controlAnim = GetComponent<AnimationController>();
        move = FindObjectOfType<PlayerMovement>();
        pStat = FindObjectOfType<PlayerAttack>();
        ren = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        stat = GetComponent<EnemyStat>();
        rb = GetComponent<Rigidbody2D>();
        ai = GetComponent<EnemyAI>();

        normal = ren.material;

        if (colliderIgnore) Physics2D.IgnoreCollision(colliderIgnore, player.GetComponent<Collider2D>());
    }
    IEnumerator _TakeDamage(bool isBlocked){
        if (moveAnim)
            moveAnim.forceStop = true;

        if (controlAnim)
            controlAnim.currentIndex = -1;

        if (ai != null) 
            ai.forceStop = true;

        StartCoroutine(Knockback(isBlocked));

        Instantiate(woundEffect, transform).transform.rotation = Quaternion.Euler(0, move.isHorizontal ? 180 : 0, 0);

        ren.material = flash;
        yield return new WaitForSeconds(.1f);
        ren.material = normal;

        ren.sprite = hurtSprite;
        yield return new WaitForSeconds(.5f);
        ren.sprite = defaultSprite;

        if (moveAnim)
            moveAnim.forceStop = false;

        if (controlAnim)
            controlAnim.currentIndex = 0;

        if (ai != null) ai.forceStop = false;
        rb.velocity = Vector2.zero;
    }

    public void TakeDamage(int dmg, bool isCrit, bool isBlocked, bool invokeAction = true){
        if (!forceStop){
            if (vulnerable)
            {
                stat.Health -= dmg;
                if (destroyed == true) { return; }
                if (stat.Health <= 0 && !destroyed)
                {
                    onNoHealth?.Invoke();

                    Destroyed();
                }

                FindObjectOfType<AudioManager>()?.Play("Hurt"); //Play sound

                if (invokeAction) onTakeDamage?.Invoke();
                if (coroutine != null)
                    StopCoroutine(coroutine);

                coroutine = StartCoroutine(_TakeDamage(isBlocked));
                InstantiateDamage(dmg, isCrit, isBlocked);
            }
        }
    }

    IEnumerator Knockback(bool isBlocked){
        float rate = pStat.attacks[pStat.currentAttackIndex - 1].knockbackRate;
        rate /= isBlocked ? 10 : 1;

        vulnerable = false;
        rbCollider.enabled = true;

        Vector2 force = move.isHorizontal ? Vector2.left + Vector2.left * rate : Vector2.right + Vector2.right * rate;
        rb.AddForce(force, ForceMode2D.Impulse);

        SpriteRenderer ren = FindObjectOfType<Player>().GetComponent<SpriteRenderer>();
        float mul = ren.flipX ? -1 : 1;

        if (spin == true){
            LeanTween.rotateZ(gameObject, transform.rotation.x - 720 * mul, knockbackRate / 1.5f).setEase(LeanTweenType.easeOutExpo);
        }

        yield return new WaitForSeconds(knockbackRate / 1.5f);
        rbCollider.enabled = false;
        vulnerable = true;
    }

    void InstantiateDamage(int dmg, bool isCrit, bool isBlocked){
        GameObject _obj = !isCrit ? damageText : critDamageText;
         _obj = !isBlocked ? _obj : blockedDamageText;

        GameObject obj = Instantiate(_obj, GameObject.Find("Enemy's Health").transform);

        RectTransform rect = obj.GetComponent<RectTransform>();
        TextMeshProUGUI txt = obj.GetComponentInChildren<TextMeshProUGUI>();

        txt.text = dmg.ToString();
        rect.position = new Vector3(transform.position.x, transform.position.y + yOffset + UnityEngine.Random.Range(-.5f, .5f), transform.position.z);
    }

    void Destroyed(){

    }
}
