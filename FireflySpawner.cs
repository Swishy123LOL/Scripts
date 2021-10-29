using UnityEngine;

public class FireflySpawner : MonoBehaviour
{
    public GameObject obj;
    public int max,min;
    public int grid;
    int n;
    BoxCollider2D col;
    void Awake(){
        col = GetComponent<BoxCollider2D>();
        n = Random.Range(min,max);
        int a = grid*grid/n;
        int b = 0; int c = 0;
        
        for (int i = 0; i < n*a; i++)
        {
            int d = Random.Range(0, a);
            if (b > grid) {
                b = 0;
                c++;
            }
            if (c > grid){
                c = 0;
            }
            Vector2 pos = new Vector2(col.bounds.min.x + ((Mathf.Abs(col.bounds.max.x - col.bounds.min.x))/grid*b), col.bounds.max.y - ((Mathf.Abs(col.bounds.max.y - col.bounds.min.y))/grid*c)); //oh god...
            if (d == 0) Instantiate(obj, pos, Quaternion.identity, transform);

            b++;
        }
    }
}
