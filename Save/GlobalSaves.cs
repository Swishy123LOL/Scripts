using UnityEngine;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;

public class GlobalSaves : MonoBehaviour
{
    string o = "gb";
    void Awake() {
        if (SaveGame.Load<bool>(o + "1", false) == false) {
            SceneManager.LoadScene(1);
            //SaveGame.Save<bool>(o + "1", true);
        }
    }
}
