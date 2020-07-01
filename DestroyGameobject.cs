using System.Collections;
using UnityEngine;

public class DestroyGameobject : MonoBehaviour
{
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait(time));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
