using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
   public Animator AttackAnimator;
    public GameObject AttackZone;
    public LayerMask Attackable;

    public Animator BossAnimator;

    float speed = 2f;

    void Update ()
    {
        if (PlayerAttack.isAttack1 == true)
        {
            AttackAnimator.SetBool("isAttack", true);
        }
    }

    public void SetBool()
    {
        AttackAnimator.SetBool("isAttack", false);
    }

    #region Attack
    public void attack()
    {
        Vector3 pos = AttackZone.transform.position;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, 1f, Attackable);

        if (colInfo != null && BossAnimator.GetBool("IsAttack") == false)
        {
            Debug.Log("Hit?");
            colInfo.GetComponent<Boss>().TakeDamage();
        }
    }

    #endregion

    #region Wait(Ienumerator)
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
    #endregion
}
