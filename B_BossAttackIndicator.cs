using UnityEngine;

public class B_BossAttackIndicator : MonoBehaviour
{
    public int count;
    public bool direction;
    SpriteRenderer ren;
    AnimationCreator anim;
    void Start(){
        ren = GetComponent<SpriteRenderer>();
        anim = GetComponent<AnimationCreator>();

        int dir = (direction)? 1 : -1;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = new GameObject(gameObject.name);
            obj.transform.position = new Vector2(transform.position.x + ren.size.x * (i+1) * dir, transform.position.y);
            obj.transform.parent = transform;

            SpriteRenderer _ren = obj.AddComponent<SpriteRenderer>();
            _ren.sprite = ren.sprite;
            _ren.sortingOrder = 2;

            AnimationCreator _anim = obj.AddComponent<AnimationCreator>();
            _anim.StopAnimation();
            
            _anim.sprites = anim.sprites;
            _anim.stepTime = anim.stepTime;
            _anim.delay += .2f * (i+1);

            _anim.PlayAnimation();
        }
    }
}
