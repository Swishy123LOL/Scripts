using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Properties")]
    public KeyCode[] attackKeys;
    public Image[] attackIcons;
    public Sprite defIcon;
    public Sprite selectIcon;

    [Header("Other")]
    public int currentAttackIndex = 0;
    public bool forceEnable;

    [Header("Player's Attacks")]
    public AttackProperties[] attacks;

    Player player;
    float time;

    public int lastDamage;

    [Serializable]

    public struct AttackProperties
    {
        public float cooldown;
        public int damage;
        [Range(0, 30)]
        public int knockbackRate;
        [Range(0, 100)]
        public int critChance;
        public string name;
        public bool isReady;
    }

    void Start()
    {
        player = GetComponent<Player>();

        attackIcons = new Image[2];
        attackIcons[0] = GameObject.Find("AttackIcon1").GetComponent<Image>();
        attackIcons[1] = GameObject.Find("AttackIcon2").GetComponent<Image>();
    }

    void Update(){
        foreach (var key in attackKeys)
        {
            if (Input.GetKeyDown(key)){
                currentAttackIndex = Array.IndexOf(attackKeys, key) + 1;
                foreach (var icon in attackIcons)
                {
                    icon.sprite = defIcon;
                }

                attackIcons[currentAttackIndex - 1].sprite = selectIcon;
            }
        }

        if (Input.GetKey(KeyCode.Space)){
            if (!DialogueManager.IsPlaying && !PlayerSettings.IsPlaying || forceEnable)
            {
                if (attacks[currentAttackIndex - 1].isReady) 
                {
                    Attack attack = FindObjectOfType<Attack>();
                    Action<string> att = attack.ExecuteAttack;

                    att(attacks[currentAttackIndex - 1].name);
                    attacks[currentAttackIndex - 1].isReady = false;

                    StartCoroutine(Cooldown(currentAttackIndex - 1));
                }
            }
        }
    }

    IEnumerator Cooldown(int index)
    {
        yield return new WaitForSeconds(attacks[index].cooldown);
        attacks[index].isReady = true;
    }
}
