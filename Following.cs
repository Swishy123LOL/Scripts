using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    public Transform player;

    public float smoothTime = 0.5f;

    Vector2 velocity = Vector2.zero;

    bool isFollowing = true;

    public float Distance;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing == true)
        {
            transform.position = Vector2.SmoothDamp(transform.position, player.position, ref velocity, smoothTime);
        }

        float dis = Vector2.Distance(transform.position, player.position);

        if (dis <= Distance)
        {
            isFollowing = false;
        }

        else
        {
            isFollowing = true;
        }
 
    }
}
