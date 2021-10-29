using UnityEngine;
using System.Collections;

public class CharacterMoveAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public float stepTime;
    bool isMoving;
    bool play;
    public bool forceStop;
    public bool forceStopForceStop; //please... DON'T ask, my system is flawless
    public bool forceStopForceStopForceStop;
    SpriteRenderer ren;
    void Start(){
        StartCoroutine(MovingCheck());
        ren = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if (isMoving && !play){
            StartCoroutine(PlayAnimation());
            play = true;
        }
    }

    IEnumerator PlayAnimation(){
        int i = 0;
        while (true)
        {
            if (!forceStop && !forceStopForceStop && !forceStopForceStopForceStop){
                ren.sprite = sprites[i]; i++;
                if (i % sprites.Length == 0){
                    i = 0;
                }

                yield return new WaitForSeconds(stepTime);
                if (isMoving == false){ 
                    play = false; 
                    ren.sprite = sprites[0];
                    break;
                }
            }

            yield return null;
        }        
    }
    IEnumerator MovingCheck(){
        while (true)
        {
            Vector2 pos = transform.position;
            yield return new WaitForSeconds(.1f);
        
            isMoving = pos != (Vector2)transform.position;
        }
    }
}
