using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Sprite FowardSprite;
    public Sprite BackSprite;
    [HideInInspector]
    public List<Vector2> positions;
    [HideInInspector]
    public List<bool> IsFoward;
    [HideInInspector]
    public List<Vector2> movingCheck;
    [HideInInspector]
    public PlayerMovement playerMovement;
    public int Delay = 15;
    void Start()
    {
        positions = new List<Vector2>();
        movingCheck = new List<Vector2>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        movingCheck.Add(playerMovement.transform.position);
        if (FowardSprite == null && BackSprite == null)
        {
            Debug.LogWarning(gameObject.name + " is missing sprite ??");
        }
        if ((FowardSprite == null && BackSprite != null) || (FowardSprite != null && BackSprite == null))
        {
            Debug.LogWarning(gameObject.name + " is missing one sprite ??");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (playerMovement.IsMoving == true)
        {
            Follow();
            Record();
        }
        movingCheck.Add(playerMovement.transform.position);
        if (movingCheck.Count > 2)
        {
            movingCheck.RemoveAt(0);
        }
        if (movingCheck[0].x == movingCheck[1].x || movingCheck[0].y == movingCheck[1].y)
        {
            playerMovement.IsMoving = false;
        }
        if (movingCheck[0].x != movingCheck[1].x || movingCheck[0].y != movingCheck[1].y)
        {
            playerMovement.IsMoving = true;
        }
    }

    void Record()
    {
        positions.Add(playerMovement.transform.position);
        if (FowardSprite != null && BackSprite != null)
        {
            IsFoward.Add(playerMovement.IsFoward);
        }
    }

    void Follow()
    {
        if (positions.Count > Delay)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
            if (FowardSprite != null && BackSprite != null)
            {
                if (IsFoward[0] == true)
                {
                    GetComponent<SpriteRenderer>().sprite = FowardSprite;
                }
                else
                    GetComponent<SpriteRenderer>().sprite = BackSprite;
                IsFoward.RemoveAt(0);
            }
        }
    }
}
