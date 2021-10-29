using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AnimationCreator : MonoBehaviour
{
    [Header("Animation")]
    public float stepTime;
    public float delay;
    public Sprite[] sprites;
    [Header("Properties")]
    public bool playOnStart = true;
    public bool playOnce;
    public bool destroyAfterPlay;
    public bool reverse;
    public bool randomizeDelay;
    public bool UI_MODE;
    public bool setNativeSize = true;
    public Action OnEndAnimation;

    bool playing;
    public int i;
    SpriteRenderer ren;
    Image image;

    void Start(){
        if (!UI_MODE) ren = GetComponent<SpriteRenderer>();
        else if (UI_MODE) image = GetComponent<Image>();

        if (randomizeDelay) delay = UnityEngine.Random.Range(0f, 1f);
        if (playOnStart) StartCoroutine(_PlayAnimation());
    }

    public void PlayAnimation(int iterations = int.MaxValue){
        if (!playing)
            StartCoroutine(_PlayAnimation(iterations));
        playing = true;
    }

    public void StopAnimation(){
        playing = false;
        StopAllCoroutines();
    }

    IEnumerator _PlayAnimation(int iterations = int.MaxValue){
        yield return new WaitForSecondsRealtime(delay);

        i = (reverse)? sprites.Length - 1 : 0;
        for (int o = 0; o < iterations; o++)
        {
            try
            {
                if (!UI_MODE) ren.sprite = sprites[i];
                else if (UI_MODE)
                {
                    image.sprite = sprites[i];
                    if (setNativeSize) image.SetNativeSize();
                }

                i += (reverse)? -1 : 1;

                if (i % sprites.Length == 0){
                    if (playOnce)
                        playing = false;

                    OnEndAnimation?.Invoke();

                    if (destroyAfterPlay) Destroy(gameObject);
                    if (playOnce) break;
                    i = (reverse)? sprites.Length - 1 : 0;
                }
            }
            catch (Exception e)
            {
                Debug.Log(string.Format("An error has occurred while trying to play the animation{2}Gameobj: {0}{2}{1}", gameObject.name, e, Environment.NewLine));
                throw;
            }

            yield return new WaitForSecondsRealtime(stepTime);
        }
    }        
    
    
}
