using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss_Attack1 : StateMachineBehaviour
{
    public Transform player;
    public Transform rb;
    public Transform Test;

    Vector2 velocity = Vector2.zero;

    public float speed = 2f;

    public float time;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Test = GameObject.FindGameObjectWithTag("Test").transform;

        rb = animator.GetComponent<Transform>();

        time = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Test.position = new Vector2(animator.transform.position.x, player.position.y);

        animator.transform.position = Vector2.SmoothDamp(animator.transform.position , Test.position, ref velocity, 0.5f);

        time += 1 * Time.fixedDeltaTime;

        if (time >= 2)
        {
            animator.SetBool("1", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
