using UnityEngine;

public class Field_Behaviour1 : MonoBehaviour
{
    public SpriteRenderer sunset;
    public Transform start, end;
    PlayerMovement move;
    Transform player;
    float dis, crr = 1;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        move = FindObjectOfType<PlayerMovement>();
        dis = end.position.x - start.position.x;
    }

    void Update()
    {
        if (move.movement.x > 0 && (1 - (player.position.x - start.position.x) / dis) < crr)
        {
            if (player.position.x > start.position.x && crr <= 1)
            {
                crr = 1 - (player.position.x - start.position.x) / dis;
                sunset.color = new Color(1, 1, 1, crr);
            }
        }
    }
}
