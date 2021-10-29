using UnityEngine;

public class House_Behaviour : MonoBehaviour
{
    public void Option1()
    {
        SaveManager._special[0] = "1";
    }

    public void Option2()
    {
        SaveManager._special[0] = "2";
    }
}
