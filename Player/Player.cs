using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerHealth = 50;
    public int PlayerDefense = 8;
    public int PlayerAttack = 10;
    [Space]
    public string PlayerWeapon;
    public string PlayerArmor;
    
    public string Sound;

    void Start()
    {
        
    }

    
    void Awake()
    {
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveManager.Save();
            Debug.Log("Saved !");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveManager.isLoaded = false;
            SaveManager.Load();
            Debug.Log("Loaded !");
        }
    }
}
