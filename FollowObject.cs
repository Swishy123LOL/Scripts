using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject ObjectToFollow;
    public bool AlsoFollowZ;
    public bool JustFollow;

    public float OffsetX;
    public float OffsetY;

    public float XPercent;
    public float YPercent;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (AlsoFollowZ == false || JustFollow == false)
        {
            transform.position = new Vector3(ObjectToFollow.transform.position.x * XPercent + OffsetX, ObjectToFollow.transform.position.y * YPercent + OffsetY, transform.position.z);
        }

        if (AlsoFollowZ == true || JustFollow == false)
        {
            transform.position = new Vector3(ObjectToFollow.transform.position.x * XPercent + OffsetX, ObjectToFollow.transform.position.y * YPercent + OffsetY, ObjectToFollow.transform.position.z);
        }

        if (JustFollow == true)
        {
            transform.position = new Vector3 (ObjectToFollow.transform.position.x, ObjectToFollow.transform.position.y, transform.position.z);
        }
    }
}
