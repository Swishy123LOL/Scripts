using UnityEngine;
using System;
public class BulletStat : MonoBehaviour
{
    public Action onCollidePlayer;
    public int damage;
    public int direction;
    public float[] p;
    public bool destroyBulletWhenCollide;

    void Start()
    {
        if (destroyBulletWhenCollide)
            onCollidePlayer += () => Destroy(gameObject);
    }
}
