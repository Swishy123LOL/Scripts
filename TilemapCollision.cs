using UnityEngine;

public class TilemapCollision : MonoBehaviour
{
    [Header("Bolleans")]
    public bool Reverse;
    public bool UseSortingOrder;
    Renderer Object;
    [Header("Properties")]
    public Renderer[] rendererAffected;
    public int[] rendererSortingOrder;
    int defaultID;
    int[] defaultSortingRenderAffected;
    int[] defaultRendererSortingOrder;
    void Start()
    {
        Object = gameObject.GetComponentInParent<Renderer>();
        defaultID = Object.sortingLayerID;
        defaultRendererSortingOrder = new int[rendererAffected.Length];
        if (Reverse == true)
        {
            Object.sortingLayerID = 0;
            foreach (Renderer ren in rendererAffected)
            {
                ren.sortingLayerID = 0;
            }
        }
        if (rendererAffected.Length != 0)
        {
            defaultSortingRenderAffected = new int[rendererAffected.Length];
            for (int i = 0; i < rendererAffected.Length; i++)
            {
                defaultSortingRenderAffected[i] = rendererAffected[i].sortingLayerID;
            }
        }
        if (UseSortingOrder == true)
        {
            for (int i = 0; i < rendererSortingOrder.Length; i++)
            {
                defaultRendererSortingOrder[i] = rendererAffected[i].sortingOrder;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (rendererAffected.Length != 0)
        {
            for (int i = 0; i < rendererAffected.Length; i++)
            {
                if (Reverse == true)
                {
                    rendererAffected[i].sortingLayerID = defaultSortingRenderAffected[i];
                }
                else
                    rendererAffected[i].sortingLayerID = 0;
                if (UseSortingOrder == true)
                {
                    for (int o = 0; o < rendererAffected.Length; o++)
                    {
                        if (o+1 > rendererSortingOrder.Length)
                        {
                            break;
                        }
                        rendererAffected[o].sortingOrder = rendererSortingOrder[o];
                    }
                }
            }
        }
        
        if (Reverse == true)
        {
            Object.sortingLayerID = defaultID;
        }
        else
            Object.sortingLayerID = 0;
        
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (rendererAffected.Length != 0)
        {
            for (int i = 0; i < rendererAffected.Length; i++)
            {
                if (Reverse == true)
                {
                    rendererAffected[i].sortingLayerID = 0;
                }
                else
                    rendererAffected[i].sortingLayerID = defaultSortingRenderAffected[i];
                if (UseSortingOrder == true)
                {
                    for (int p = 0; p < rendererAffected.Length; p++)
                    {
                        rendererAffected[i].sortingOrder = defaultRendererSortingOrder[i];
                    }
                }
            }
        }

        if (Reverse == true)
        {
            Object.sortingLayerID = 0;
        }
        else
            Object.sortingLayerID = defaultID;

    }

}
