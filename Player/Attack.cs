using System.Collections;
using System.Reflection;
using UnityEngine;

public class Attack : MonoBehaviour
{
    PlayerMovement move;
    SpriteRenderer ren;
    BoxCollider2D col;
    Rigidbody2D rb;

    [Header("Solargress's Properties")]
    public AnimationCreator punch1;
    public AnimationCreator punch2;
    public AttackCollision aCol;
    public BoxCollider2D collisionCollider;
    public Animator colliderAnim;
    public Transform colliderObj;
    public GameObject ramFx;
    public LayerMask filter;
    Sprite pre;
    bool p;

    void Start()
    {
        move = GetComponent<PlayerMovement>();
        ren = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        punch1.OnEndAnimation += EndAttack;
        punch2.OnEndAnimation += EndAttack;
    }

    void EndAttack()
    {
        p = false;
        aCol.forceStop = true;
        ren.flipX = false;
        ren.sprite = pre;
        move.forceStop = false;
        colliderAnim.Play("Default");
    }
    public void ExecuteAttack(string nm){
        MethodInfo mi = this.GetType().GetMethod(nm);
        mi.Invoke(this, new object[] {});
    }

    #region Yuki's attacks
    public void Punch(){
        int r = Random.Range(1, 3);

        if (!p)
        {
            p = true;
            aCol.forceStop = false;
            colliderAnim.Play("Yuki_Punch1");
            ren.flipX = move.isHorizontal;
            colliderObj.rotation = Quaternion.Euler(0, move.isHorizontal ? 180 : 0, 0);
            pre = ren.sprite;

            move.forceStop = true;
            move.movement = Vector2.zero;

            if (r == 1)
                punch1.PlayAnimation();

            else
                punch2.PlayAnimation();
        }
    }

    public void Ram()
    {
        StartCoroutine(_Ram());
    }

    public IEnumerator _Ram(){
        float waitTime = 0.5f;
        Vector2 pos = transform.position;
        int mul = move.isHorizontal ? -1 : 1;

        rb.velocity = Vector2.zero;
        aCol.forceStop = false; 
        move.enabled = false;
        col.enabled = false;
        collisionCollider.enabled = true;

        LeanTween.moveY(gameObject, transform.position.y + 0.5f, 0.3f).setEase(LeanTweenType.easeOutSine);
        LeanTween.rotateZ(gameObject, -90 * mul, 0.3f).setEase(LeanTweenType.easeOutSine);
        yield return new WaitForSeconds(0.3f);

        LeanTween.moveY(gameObject, transform.position.y - 0.5f, 0.5f).setEase(LeanTweenType.easeOutBounce);

        RaycastHit2D hit = Physics2D.BoxCast(pos, new Vector2(1.375f, 1.375f), 90 * mul, Vector2.right * mul, 20 * waitTime, filter);
        int count = Mathf.FloorToInt(waitTime * 20 / 1.5f);

        if (hit)
        {
            waitTime = Mathf.Abs((hit.point.x - transform.position.x) / 20);
            count = Mathf.FloorToInt(Mathf.Abs(hit.point.x - transform.position.x) / 1.5f);
        }

        if (count < 2)
            yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < count; i++)
        {
            Instantiate(ramFx, new Vector3(transform.position.x + 1.5f * (i + 1) * mul, transform.position.y), Quaternion.identity).GetComponent<SpriteRenderer>().sprite = ren.sprite;
            yield return new WaitForSeconds(0.03f);
        }

        col.enabled = true;
        rb.velocity = new Vector2(20 * mul, rb.velocity.y);

        yield return new WaitForSeconds(waitTime);
        col.enabled = false;
        move.enabled = true;
        aCol.forceStop = true;
        rb.velocity = Vector2.zero;
        collisionCollider.enabled = false;

        LeanTween.moveY(gameObject, transform.position.y + 0.5f, 0.3f).setEase(LeanTweenType.easeOutSine);
        LeanTween.rotateZ(gameObject, 0, 0.3f).setEase(LeanTweenType.easeOutSine);
        yield return new WaitForSeconds(0.3f);

        LeanTween.moveY(gameObject, transform.position.y - 0.5f, 0.5f).setEase(LeanTweenType.easeOutBounce);
        yield return new WaitForSeconds(0.5f);

        col.enabled = true;
    }

    #endregion
}
