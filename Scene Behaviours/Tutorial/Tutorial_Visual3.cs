using System.Collections;
using UnityEngine;

public class Tutorial_Visual3 : MonoBehaviour
{
    public GameObject obj, obj2;

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(5);
        obj.SetActive(true);
        obj2.SetActive(true);
    }

    void Start()
    {
        StartCoroutine(enumerator());
    }
}
