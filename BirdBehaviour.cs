using UnityEngine;
using System.Collections;

public class BirdBehaviour : MonoBehaviour
{
    public float range;
    public float range2;
    public AnimationCreator[] anims;
    public State state;
    bool changed;
    float time;
    Player player;
    public enum State
    {
        Idling,
        Pecking,
        Flying,
        Moving
    };

    void Start(){
        anims[2].OnEndAnimation += delegate {
            state = State.Flying;
            anims[3].PlayAnimation();
            LeanTween.move(gameObject, new Vector2(transform.position.x + 50 * transform.localScale.x, transform.position.y + 10), 2).setEaseInSine();
        };
        
        player = FindObjectOfType<Player>();
        state = State.Idling;
        range += Random.Range(0f, 1f);

        StartCoroutine(Move());
    }

    void Update(){
        time += Time.deltaTime;
        if (time > 1 && state != State.Moving) {
            time = 0;

            if (state == State.Pecking){
                ChangeAnim(0);
                state = State.Idling;
            }

            else if (state == State.Idling){
                if (Random.Range(0, 5) == 0 && !changed){
                    ChangeAnim(1);
                    state = State.Pecking;
                }
            }
        }

        if (Vector2.Distance(transform.position, player.transform.position) < range && !changed){
            changed = true;
            GetComponent<SpriteRenderer>().sortingLayerName = "Top Middle";
            StopAllCoroutines();
            ChangeAnim(2);
        }
    }

    IEnumerator Move(){
        while (true)
        {
            if (Random.Range(0, 7) == 0){
                state = State.Moving;
                int steps = Random.Range(3, 10);

                for (int i = 1; i < steps+1; i++)
                {
                    float r1 = Random.Range(-range2, range2); float r2 = Random.Range(-range2, range2); 
                    float s = (r1<0)? -1 : 1;
                    Vector2 pos = new Vector2(transform.position.x + r1*i, transform.position.y + r2*i);

                    LeanTween.scaleX(gameObject, s, .2f).setEaseOutSine();
                    LeanTween.move(gameObject, pos, .2f);
                    ChangeAnim(4);
            
                    yield return new WaitForSeconds(.5f);
                }

                state = State.Idling;
                ChangeAnim(0);
            }
            yield return new WaitForSeconds(1);
        }
    }
    void ChangeAnim(int i, int o = int.MaxValue){
        foreach (var anim in anims)
        {
            anim.StopAnimation();
        }

        anims[i].PlayAnimation(o);
    }
}
