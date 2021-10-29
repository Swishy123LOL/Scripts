using UnityEngine;

public class GameData : MonoBehaviour
{
    public static string Script;

    void Awake()
    {
        Script = CustomSave.Load("Script", "Savedata/", ".txt");
    }
    public struct Unique
    {
        public bool[] CScene;
        public string[] special;
    }

    public struct Normal
    {
        public bool Created;
        public int PlayerHealth;
        public int areaIndex;
        public float Position_X;
        public float Position_Y;
        public float Position_Z;
        public float Time;
        public bool facingX;
        public bool facingY;
    }

    public struct Settings
    {
        public float masterVolume;
        public bool isMuted;
    }
}
