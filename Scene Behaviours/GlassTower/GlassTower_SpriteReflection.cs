using UnityEngine;

public class GlassTower_SpriteReflection : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public GameObject prefab;

    void Awake(){
        foreach (var sprite in sprites)
        {
            GameObject obj = Instantiate(prefab, sprite.transform);
            SpriteRenderer sprite1 = obj.AddComponent<SpriteRenderer>();

            sprite1.sprite = sprite.sprite;
            sprite1.flipY = !sprite.flipY;
            sprite1.sortingOrder = -1;

            sprite1.transform.position = new Vector3(sprite.transform.position.x,
            sprite.transform.position.y - sprite.bounds.size.y,
            sprite.transform.position.z);

            sprite1.color = new Color(1, 1, 1, .05f);
        }
    }
}
