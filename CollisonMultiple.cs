using UnityEngine;

public class CollisonMultiple : MonoBehaviour
{
    [Header("Bolleans")]
    public bool Collision;
    public bool HaveAnotherCollision;
    SpriteRenderer Object;
    [Header("Properties")]
    public SpriteRenderer[] ObjectAffected;
    public int[] rendererAffectedSortingOrder;
    public int rendererSortingOrder;
    int defaultSortingOrder;
    int[] defaultSortingOrderAffected;
    void Start()
    {
        Object = gameObject.GetComponentInParent<SpriteRenderer>();
        defaultSortingOrder = Object.sortingOrder;
        if (ObjectAffected.Length != 0)
        {
            defaultSortingOrderAffected = new int[ObjectAffected.Length];
            for (int i = 0; i < ObjectAffected.Length; i++)
            {
                defaultSortingOrderAffected[i] = ObjectAffected[i].sortingOrder;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Object.sortingOrder = rendererSortingOrder;
        if (ObjectAffected.Length != 0)
        {
            for (int i = 0; i < ObjectAffected.Length; i++)
            {
                ObjectAffected[i].sortingOrder = rendererAffectedSortingOrder[i];
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        Object.sortingOrder = defaultSortingOrder;
        if (ObjectAffected.Length != 0)
        {
            for (int i = 0; i < ObjectAffected.Length; i++)
            {
                ObjectAffected[i].sortingOrder = defaultSortingOrderAffected[i];
            }
        }
    }
}
