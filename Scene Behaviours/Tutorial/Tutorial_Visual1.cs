using UnityEngine.UI;
using UnityEngine;

public class Tutorial_Visual1 : MonoBehaviour
{
    public SpriteRenderer key1, key2;
    public Sprite newKey, oldKey;
    public RectTransform text1, text2;
    float oldPosY;

    void Start()
    {
        oldPosY = text1.position.y;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            key1.GetComponent<AnimationCreator>().StopAnimation();
            key2.GetComponent<AnimationCreator>().StopAnimation();
            key1.sprite = newKey;
            key2.sprite = oldKey;
            key1.GetComponent<AnimationCreator>().PlayAnimation();

            text1.position = new Vector3(text1.position.x, oldPosY - .3f);
            text2.position = new Vector3(text2.position.x, oldPosY);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            key1.GetComponent<AnimationCreator>().StopAnimation();
            key2.GetComponent<AnimationCreator>().StopAnimation();
            key2.sprite = newKey;
            key1.sprite = oldKey;
            key2.GetComponent<AnimationCreator>().PlayAnimation();

            text2.position = new Vector3(text2.position.x, oldPosY - .3f);
            text1.position = new Vector3(text1.position.x, oldPosY);
        }
    }
}
