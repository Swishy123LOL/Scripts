using UnityEngine.SceneManagement;
using UnityEngine;

//useful when doing animations
public class LoadGame : MonoBehaviour
{
    public void Load(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPrevious()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
