using UnityEngine;

public class LineRender : MonoBehaviour
{
    public BoxCollider2D[] boxCollider2D;
    LineRenderer[] LineRenderers;
    Transform[] transforms;
    public Material lineMaterial;
    public GameObject prefab;
    public GameObject sortingGroup;
    void RenderBox(BoxCollider2D collider, Color color, LineRenderer lineRenderer, Transform _transform){
        Vector2[] vertex = GetVertex(collider, _transform);

        lineRenderer.positionCount = vertex.Length;
        lineRenderer.startWidth = .04f;
        lineRenderer.material = lineMaterial;
        lineRenderer.sortingLayerID = sortingGroup.GetComponent<SpriteRenderer>().sortingLayerID;

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        for (int i = 0; i < vertex.Length; i++)
        {
            lineRenderer.SetPosition(i, vertex[i]);
        }
    }

    Vector2[] GetVertex(BoxCollider2D collider, Transform transform){
        Vector2[] vertex = new Vector2[5];
        
        vertex[0] = new Vector2(-collider.bounds.extents.x + (collider.offset.x + transform.position.x),
        -collider.bounds.extents.y + (collider.offset.y + transform.position.y));

        vertex[1] = new Vector2(collider.bounds.extents.x + (collider.offset.x + transform.position.x), 
        -collider.bounds.extents.y + (collider.offset.y + transform.position.y));

        vertex[2] = new Vector2(collider.bounds.extents.x + (collider.offset.x + transform.position.x), 
        collider.bounds.extents.y + (collider.offset.y + transform.position.y));

        vertex[3] = new Vector2(-collider.bounds.extents.x + (collider.offset.x + transform.position.x), 
        collider.bounds.extents.y + (collider.offset.y + transform.position.y));


        vertex[4] = new Vector2(-collider.bounds.extents.x + (collider.offset.x + transform.position.x),
        -collider.bounds.extents.y + (collider.offset.y + transform.position.y));
        return vertex;
    }

    public void RenderLine(){
        boxCollider2D = new BoxCollider2D[FindObjectsOfType<BoxCollider2D>().Length];
        transforms = new Transform[boxCollider2D.Length];
        LineRenderers = new LineRenderer[boxCollider2D.Length];

        boxCollider2D = FindObjectsOfType<BoxCollider2D>();
        
        for (int i = 0; i < boxCollider2D.Length; i++)
        {
            transforms[i] = boxCollider2D[i].transform;
        }

        for (int i = 0; i < boxCollider2D.Length; i++)
        {
            GameObject _gameObject = Instantiate(prefab, transform);

            Color color = (boxCollider2D[i].isTrigger == true)? Color.red : ((boxCollider2D[i].gameObject.name == "Character")?Color.yellow : Color.green);
            //Check when box collider has trigger, set color to red
            //Otherwise, if gameobject's name is "Character", set color to yellow
            //If not, set color to green
            
            LineRenderers[i] = _gameObject.AddComponent<LineRenderer>();
            RenderBox(boxCollider2D[i], color, LineRenderers[i], transforms[i]);
        }
    }

    public void RemoveLine(){
        boxCollider2D = new BoxCollider2D[0];
        transforms = new Transform[0];

        foreach (var lineRender in LineRenderers)
        {
            Destroy(lineRender.gameObject);
        }

        LineRenderers = new LineRenderer[0];
    }
}
