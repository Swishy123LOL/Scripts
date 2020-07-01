using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb2d;
    float horizontal;

    bool facing = true;
    bool isTime = false;

    Vector2 movement;

    public Animator Animator;

    Color defaultColor;
    Color Invisible;

    public float time = 1;

    public SpriteRenderer[] PlayerspriteRenderer;

    void Start()
    {

    }
    void Update ()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 1.6f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= 1.6f;
        }

        if (PlayerAttack.isAttack1 == true)
        {
            Animator.SetBool("isAttack", true);
            StartCoroutine(Wait());
            PlayerAttack.isAttack1 = false;
        }
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTime == false)
        {
            time -= Time.deltaTime;
        }

        if (time == 0)
        {
            isTime = true;
        }

        movement.Normalize();
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);

        horizontal = Mathf.Abs(movement.x);
        if (DialogueManager.IsPlaying == false || CameraTransition_TP.isPlaying == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
            movement.y = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime;
        }
        if (DialogueManager.IsPlaying == true || CameraTransition_TP.isPlaying == true)
        {
            movement.x = 0;
            movement.y = 0;
        }

            if (movement.x > 0 && !facing)
        {
            Flip();
        }
        else if (movement.x < 0 && facing)
        {
            Flip();
        }

        if (horizontal > 0 || Input.GetKeyDown(KeyCode.S))
        {
            Animator.SetBool("IsFront", true);
            Animator.SetBool("IsBack", false);
        }

        if (movement.y > 0)
        {
            Animator.SetBool("IsFront", false);
            Animator.SetBool("IsBack", true);
        }
    }

    private void Flip()
    {
        facing = !facing;

        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
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

}
