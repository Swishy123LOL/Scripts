using UnityEngine;
using System.Collections;
public class ScrollingBackground : MonoBehaviour
{
    public GameObject prefab;
    [HideInInspector]
    public GameObject[] gameObjects;
    SpriteRenderer ren;
    public int length;
    public bool direction;
    float time;
    void Awake(){
        int mul = (direction)? 1 : -1;
        ren = GetComponent<SpriteRenderer>();
        gameObjects = new GameObject[length+1];

        for (int i = 0; i < length+1; i++)
        {
            if (prefab == null){
                GameObject obj = new GameObject(gameObject.name + i, typeof(SpriteRenderer));
                SpriteRenderer ren1 = obj.GetComponent<SpriteRenderer>();
                ren1.sortingLayerID = ren.sortingLayerID;
                ren1.sortingOrder = ren.sortingOrder;
                ren1.material = ren.material;
                ren1.sprite = ren.sprite;

                obj.transform.position = new Vector2(transform.position.x + (ren.size.x * mul * i), transform.position.y);
                obj.transform.parent = transform;
                gameObjects[i] = obj;
            }

            else {
                GameObject obj = Instantiate(prefab, new Vector2(transform.position.x + (ren.size.x * mul * i), transform.position.y), Quaternion.identity, transform);
                obj.GetComponent<SpriteRenderer>().sprite = ren.sprite;
            }
        }

        ren.sprite = null;
        StartCoroutine(Move());
    }

    void FixedUpdate(){
        int mul = (direction)? 1 : -1;
        transform.position = new Vector2(transform.position.x + Time.fixedDeltaTime * mul, transform.position.y);
    }
    
    IEnumerator Move(){
        int mul = (direction)? 1 : -1;
        float newPos = transform.position.x - ren.size.x * mul;
        while (true){
            yield return new WaitForSecondsRealtime(ren.size.x);
            transform.position = new Vector2(newPos, transform.position.y);
        }
    }
}
