using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public GameObject[] menus;
    public StatusSave[] statusSaves;
    public void LoadScene()
    {
        SceneManager.LoadScene(1);

        GameData.Normal data1 = new GameData.Normal();
        GameData.Unique data2 = new GameData.Unique();

        data1.Created = true;
        data1.areaIndex = 1;

        data2.CScene = new bool[SaveManager.csCount];
        data2.special = new string[SaveManager.specialCount];

        SaveManager.QuickSave(data1, data2);

        foreach (StatusSave statusSave in statusSaves)
        {
            Destroy(statusSave);
        }
    }

    public void MenuSwitch(int enable)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        menus[enable].SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadScene();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
