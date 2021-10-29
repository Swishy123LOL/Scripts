using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Corruption : MonoBehaviour
{
    [Header("Values")]
    [Range(0, 100)]
    public int corruptionLevel;
    public float timeStep;

    [Header("References")]
    public Material computerEffectMaterial;
    public Material corruptedMaterial;
    public Material glitchMaterial;
    public Material defMaterial;
    public GameObject[] gameObjects;
    GameObject[] _gameObjects;
    AudioManager audioManager;
    float time;
    int a, b;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Tutorial2");
        audioManager.SetVolume("Tutorial2", 0);

        foreach (var obj in gameObjects)
        {
            a += obj.GetComponent<ScrollingBackground>().gameObjects.Length;
        }

        _gameObjects = new GameObject[a];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            ScrollingBackground bg = gameObjects[i].GetComponent<ScrollingBackground>();
            for (int o = 0; o < bg.gameObjects.Length; o++)
            {
                _gameObjects[b] = bg.gameObjects[o];
                b++;
            }
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > timeStep)
        {
            time = 0;
            glitchMaterial.SetFloat("_Value", glitchMaterial.GetFloat("_Value") + Random.Range(0.5f, 0.9f));
            glitchMaterial.SetFloat("_Value2", glitchMaterial.GetFloat("_Value2") + Random.Range(0.5f, 0.8f));
        }
    }

    public void Corrupt(int level)
    {
        StartCoroutine(_Corrupt(level));
    }

    public IEnumerator _Corrupt(int level)
    {
        audioManager.Play("Static");
        if (corruptionLevel > 25) audioManager.SetVolume("Tutorial2", 0.2f);
        else
        {
            audioManager.SetVolume("Tutorial2", 0.2f);
            audioManager.SetVolume("Tutorial", 0);
        }

        GameObject.Find("RenderScreen").GetComponent<RawImage>().material = glitchMaterial;
        LeanTween.value(1f, 0f, 1f).setOnUpdate((float val) => { glitchMaterial.SetFloat("_Intensity", val); }).setEaseOutSine();
        foreach (GameObject obj in _gameObjects)
        {
            int r = Random.Range(0, 100);
            if (r < level)
            {
                obj.GetComponent<SpriteRenderer>().material = corruptedMaterial;
            }
            else
            {
                obj.GetComponent<SpriteRenderer>().material = defMaterial;
            }
        }

        yield return new WaitForSeconds(1);

        audioManager.Stop("Static");
        if (corruptionLevel > 25) audioManager.SetVolume("Tutorial2", 1);
        else audioManager.SetVolume("Tutorial", 1);

        GameObject.Find("RenderScreen").GetComponent<RawImage>().material = computerEffectMaterial;
    }
}
