using UnityEngine;
[System.Serializable]

public class PlayerItem : MonoBehaviour
{
    public string Name;
    public string Description;
    public string UseDialogue;
    
    public struct HealProperty
    {
        public int Heal;
        public bool DestroyAfterUse;
    }
}
