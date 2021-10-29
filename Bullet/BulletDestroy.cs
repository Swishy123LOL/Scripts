using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    SpriteRenderer ren;
    float timer;
    void Start(){
        ren = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if (ren.isVisible == true){
            timer = 0;
        }
        else if (ren.isVisible == false){
            timer += Time.deltaTime;
        }
        if (timer >= 5){
            Destroy(gameObject);
        }
    }
}
