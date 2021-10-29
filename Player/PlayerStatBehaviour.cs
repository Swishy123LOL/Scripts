using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatBehaviour : MonoBehaviour
{
    [Header("Slider")]
    public MenuSlider Attack;
    public MenuSlider Defend;
    public MenuSlider Health;

    [Header("Text")]
    public TextMeshProUGUI AttackCount;
    public TextMeshProUGUI DefendCount;
    public TextMeshProUGUI HealthCount;

    [Header("Weapon")]
    public string CurrentWeapon;
    string PreWeapon = "";
    public TextMeshProUGUI WeaponDisplay;
    [Space]
    public List<string> PlayerWeapons = new List<string>();
    [Space]
    public string CurrentArmor;
    string PreArmor = "";
    public TextMeshProUGUI ArmorDisplay;
    [Space]
    public List<string> PlayerArmors = new List<string>();

    [Header("Others")]
    public Color TextColor;
    public Color Grey;
    void Awake()
    {
        Health.MaxValue = FindObjectOfType<Player>().PlayerHealth;
        UpdateCount();
        UpdateWeapon();
        UpdateArmor();
    }

    public void UpdateCount()
    {
        Attack.Value = FindObjectOfType<Player>().PlayerAttack;
        Defend.Value = FindObjectOfType<Player>().PlayerDefense;
        Health.Value = FindObjectOfType<Player>().PlayerHealth;

        AttackCount.text = FindObjectOfType<Player>().PlayerAttack.ToString();
        DefendCount.text = FindObjectOfType<Player>().PlayerDefense.ToString();
        HealthCount.text = FindObjectOfType<Player>().PlayerHealth.ToString() + "/" + Health.MaxValue;
    }

    public void UpdateStat()
    {
        Attack.UpdateStat();
        Defend.UpdateStat();
        Health.UpdateStat();
        UpdateWeapon();
        UpdateArmor();
    }

    public void UpdateWeapon()
    {
        CurrentWeapon = FindObjectOfType<Player>().PlayerWeapon;
        if (PreWeapon != CurrentWeapon)
        {
            PlayerWeapons.Add(CurrentWeapon);
        }
        if (CurrentWeapon == "")
        {
            WeaponDisplay.text = "(None...)";
            WeaponDisplay.color = Grey;
        }
        else
        {
            WeaponDisplay.text = CurrentWeapon;
            WeaponDisplay.color = TextColor;
        }
        PreWeapon = CurrentWeapon;
    }
    public void UpdateArmor()
    {
        if (PreArmor != CurrentArmor)
        {
            PlayerArmors.Add(CurrentArmor);
        }
        CurrentArmor = FindObjectOfType<Player>().PlayerArmor;
        if (CurrentArmor == "")
        {
            ArmorDisplay.text = "(None...)";
            ArmorDisplay.color = Grey;
        }
        else
        {
            ArmorDisplay.text = CurrentArmor;
            ArmorDisplay.color = TextColor;
        }
        PreArmor = CurrentArmor;
    }
}
