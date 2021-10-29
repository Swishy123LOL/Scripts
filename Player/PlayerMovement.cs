using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 5f;
    public float runSpeed = 1.6f;
    [Header("Boleans")]
    public bool isHorizontal;
    public bool facingX;
    public bool facingY;
    public bool isSprint;
    public bool Stop;
    public bool sped;
    public bool IsFoward;
    public bool IsMoving;
    public bool forceStop;
    public bool forceEnable;
    [Header("Properties")]
    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite rightSprite;
    public Sprite leftSprite;
    [Space]
    public AnimationController anim;

    Rigidbody2D rb2d;
    float horizontal;

    bool facing = true;
    bool isTime = false;
    [HideInInspector]
    public Vector2 movement;

    Animator Animator;

    float time = 1;

    SpriteRenderer ren;
    [HideInInspector]
    public bool IsPlaying = false;
    float defaultRunSpeed;
    float defaultSpeed;

    public Action OnCollision;
    public bool isColliding;
    void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        ren = gameObject.GetComponent<SpriteRenderer>();
        defaultSpeed = moveSpeed;
        defaultRunSpeed = runSpeed;
        
        StartCoroutine(MovingCheck(.1f));

        ren.sprite = frontSprite;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprint = true;
            if (isSprint == true)
            {
                StartCoroutine(Accelerate());
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprint = false;
            runSpeed = defaultRunSpeed;
            moveSpeed = defaultSpeed;
        }

        if (Mathf.Abs(movement.x) > 0 && Mathf.Abs(movement.y) > 0 && sped == false)
        {
            moveSpeed *= Mathf.Sqrt(2 * Mathf.Pow(moveSpeed, 2)) / moveSpeed;
            sped = true;
        }

        float num = Mathf.Abs(movement.x) - Mathf.Abs(movement.y);

        if (num == Mathf.Abs(movement.x) && sped == true || num == Mathf.Abs(movement.y) && sped == true)
        {
            moveSpeed /= Mathf.Sqrt(2 * Mathf.Pow(moveSpeed, 2)) / moveSpeed;
            sped = false;
        }

        if (moveSpeed < defaultSpeed)
        {
            moveSpeed = defaultSpeed;
        }

        if (movement.x == 0 && movement.y == 0 && anim.frame == 1)
            anim.currentIndex = -1;
    }

    IEnumerator Accelerate()
    {
        if (isSprint == true)
        {
            for (int i = 14; i < 22; i++)
            {
                if (isSprint == false)
                {
                    runSpeed = defaultRunSpeed;
                    break;
                }
                float num = i / 20f;
                runSpeed = defaultRunSpeed * num;
                moveSpeed = defaultSpeed * runSpeed;
                yield return new WaitForSeconds(.05f);
            }
        }
    }

    void FixedUpdate()
    {
        if (Stop == true)
        {
            movement.x = 0;
            movement.y = 0;
        }

        if (isTime == false)
        {
            time -= Time.deltaTime;
        }

        if (time < 0)
        {
            isTime = true;
        }

        movement.Normalize();

        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);

        horizontal = Mathf.Abs(movement.x);
        if (!DialogueManager.IsPlaying && !IsPlaying || PlayerSettings.IsPlaying && !IsPlaying || forceEnable)
        {
            if (!forceStop)
            {
                movement.x = Mathf.FloorToInt(Input.GetAxisRaw("Horizontal")) * Time.fixedDeltaTime;
                movement.y = Mathf.FloorToInt(Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime;
            }
        }

        if (movement.y > 0)
            anim.currentIndex = 0;

        if (movement.y < 0)
            anim.currentIndex = 1;

        if (movement.x > 0 && movement.y == 0)
        {
            isHorizontal = false;
            anim.currentIndex = 2;
        }

        if (movement.x < 0 && movement.y == 0)
        {
            isHorizontal = true;
            anim.currentIndex = 3;
        }

        if (DialogueManager.IsPlaying && !IsPlaying && !forceEnable || PlayerSettings.IsPlaying && !IsPlaying && !forceEnable)
        {
            if (!forceStop)
            {
                movement.x = 0;
                movement.y = 0;
            }
        }

        if (!DialogueManager.IsPlaying || !PlayerSettings.IsPlaying || forceEnable)
        {
            if (!forceStop)
            {
                if (horizontal > 0 || movement.y < 0)
                {
                    IsFoward = false;
                }

                if (movement.y > 0)
                {
                    IsFoward = true;
                }
            }
        }
    }

    IEnumerator Wait ()
    {
        yield return new WaitForSeconds(4f / 12f);
        Animator.SetBool("isAttack", false);
    }

    public void SetBool()
    {
        Animator.SetBool("Hurt", false);
    }

    IEnumerator MovingCheck(float stepTime){
        while (true)
        {
            Vector2 pos = transform.position;
            yield return new WaitForSeconds(stepTime);
        
            IsMoving = pos != (Vector2)transform.position;
            FacingCheck(pos, transform.position);
        }
    }

    void FacingCheck(Vector2 old, Vector2 curr){
        if (old.x - curr.x < 0) { facingX = false; }
        if (old.x - curr.x > 0) { facingX = true; }
        if (old.y - curr.y < 0) { facingY = false; }
        if (old.y - curr.y > 0) { facingY = true; }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        OnCollision?.Invoke();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}
