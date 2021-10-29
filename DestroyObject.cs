using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public bool onStart;
    public float delay;
    public bool enableInstead;

    void Start(){
        if (onStart == true && !enableInstead) { Destroy(gameObject, delay); }
        if (onStart == true && enableInstead) { gameObject.SetActive(true); }
    }
    public void _Destroy(){
        if (!enableInstead) Destroy(gameObject);
        else gameObject.SetActive(true);
    }
}
