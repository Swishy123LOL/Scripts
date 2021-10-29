using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform followObject;
    public float followRateX, followRateY = 1;
    public Vector2 offset;
    public bool dontFollow;

    void FixedUpdate()
    {
        if (!dontFollow)
            transform.position = new Vector3(followObject.position.x * followRateX,
                                 followObject.position.y * followRateY,
                                 transform.position.z) + (Vector3)offset;
        else
            transform.position = new Vector3(transform.position.x * followRateX,
                                 transform.position.y * followRateY,
                                 transform.position.z) + (Vector3)offset;
    }
}
