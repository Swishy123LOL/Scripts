using UnityEngine;
[RequireComponent(typeof(EnemyCollision), typeof(SpriteRenderer))]
public class EnemyAura : MonoBehaviour
{
    public SpriteRenderer[] rens;
    public Material material2;
    public float offset;
    GameObject[] gameObjects;
    SpriteRenderer[] _rens;
    Sprite[] oldSprites;
    EnemyCollision col;
    public enum AuraState
    {
        Red,
        White
    }
    void Awake()
    {
        col = GetComponent<EnemyCollision>();
        gameObjects = new GameObject[4 * rens.Length];
        _rens = new SpriteRenderer[4 * rens.Length];
        oldSprites = new Sprite[rens.Length];
        GameObject empty = new GameObject();

        for (int j = 0; j < rens.Length; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                gameObjects[i + 4 * j] = Instantiate(empty, rens[j].transform.position, Quaternion.identity, rens[j].transform);
                SpriteRenderer ren1 = gameObjects[i + 4 * j].AddComponent<SpriteRenderer>();
                _rens[i + 4 * j] = ren1;

                ren1.sprite = rens[j].sprite;
                ren1.material = material2;
                ren1.sortingOrder = rens[j].sortingOrder - 1;
            }
        }

        for (int i = 0; i < rens.Length; i++)
        {
            gameObjects[rens.Length * i].transform.position = new Vector3(gameObjects[rens.Length * i].transform.position.x + offset, gameObjects[rens.Length * i].transform.position.y);
            gameObjects[1 + rens.Length * i].transform.position = new Vector3(gameObjects[1 + rens.Length * i].transform.position.x - offset, gameObjects[1 + rens.Length * i].transform.position.y);
            gameObjects[2 + rens.Length * i].transform.position = new Vector3(gameObjects[2 + rens.Length * i].transform.position.x, gameObjects[2 + rens.Length * i].transform.position.y + offset);
            gameObjects[3 + rens.Length * i].transform.position = new Vector3(gameObjects[3 + rens.Length * i].transform.position.x, gameObjects[3 + rens.Length * i].transform.position.y - offset);
        }
    }

    void Update()
    {
        for (int i = 0; i < rens.Length; i++)
        {
            if (rens[i].sprite != oldSprites[i])
            {
                oldSprites[i] = rens[i].sprite;
                for (int j = 0; j < 4; j++)
                {
                    gameObjects[j + 4 * i].GetComponent<SpriteRenderer>().sprite = rens[i].sprite;
                }
            }
        }
    }
    public void Change(AuraState state)
    {
        switch (state)
        {
            case AuraState.Red:
                col.forceStop = true;
                ChangeColor(Color.red);
                break;
            case AuraState.White:
                col.forceStop = false;
                ChangeColor(Color.white);
                break;
        }
    }

    void ChangeColor(Color color)
    {
        foreach (SpriteRenderer _ren in _rens)
        {
            _ren.material.SetColor("_Col", color);
        }
    }
}
