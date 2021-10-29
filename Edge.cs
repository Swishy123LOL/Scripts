using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Edge : MonoBehaviour
{
    void Awake()
    {
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();

        GameObject bounder = new GameObject("BounderCollider");
        bounder.transform.position = transform.position;
        bounder.layer = 14;

        Vector2[] points = new Vector2[poly.points.Length + 1];
        for (int i = 0; i < poly.points.Length - 1; i++)
        {
            points[i] = poly.points[i];
        }

        points[poly.points.Length] = poly.points[0];
        points[poly.points.Length - 1] = poly.points[poly.points.Length - 1];

        EdgeCollider2D edge = bounder.AddComponent<EdgeCollider2D>();
        edge.points = points;
    }
}
