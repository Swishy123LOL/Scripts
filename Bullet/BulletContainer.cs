using UnityEngine;

public class BulletContainer : MonoBehaviour
{
    public void RemoveAll() 
    {
        foreach (Transform obj in GetComponentsInChildren<Transform>())
        {
            if (obj.gameObject == gameObject) continue;
            Destroy(obj.gameObject);
        }
    }
}
