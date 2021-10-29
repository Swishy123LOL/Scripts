using System.Collections;
using TMPro;
using UnityEngine;

public class OnOffButton1 : MonoBehaviour
{
    public TextMeshProUGUI first;
    public TextMeshProUGUI second;
    public bool _Enable;
    void Update()
    {
        first.enabled = Screen.fullScreen;
        second.enabled = !Screen.fullScreen;
        if (Input.GetKeyDown(KeyCode.Space) && _Enable == true)
        {
            FindObjectOfType<PlayerSettings>().ChangeFullScreen();
        }
    }
    IEnumerator Change(float time, bool Bool)
    {
        yield return new WaitForSeconds(time);
        _Enable = Bool;
    }
    public void ChangeEnable(bool Bool)
    {
        StartCoroutine(Change(0.05f, Bool));
    }

    public void Enable(bool Bool)
    {
        enabled = Bool;
    }
}
