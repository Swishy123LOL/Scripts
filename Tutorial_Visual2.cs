using UnityEngine;

public class Tutorial_Visual2 : MonoBehaviour
{
    public SpriteRenderer key;
    public Sprite newKey;
    public RectTransform text;
    bool pressed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pressed)
        {
            pressed = true;
            key.sprite = newKey;
            key.GetComponent<AnimationCreator>().PlayAnimation();
            text.position = new Vector3(text.position.x, text.position.y - .3f);
        }
    }
}
