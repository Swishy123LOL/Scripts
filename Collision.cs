using UnityEngine;

public class Collision : MonoBehaviour
{
    SpriteRenderer Object;
    int defaultSortingOrder;

    void Start()
    {
        Object = gameObject.GetComponentInParent<SpriteRenderer>();
        defaultSortingOrder = Object.sortingOrder;
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player"){
            Object.sortingOrder = 1;
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player"){
            Object.sortingOrder = defaultSortingOrder;
        }
    }
    
}
