using System.Collections.Generic;

public static class PlayerItemManager
{
    public static List<PlayerItem> playerItems = new List<PlayerItem>();
    public static List<PlayerItem.HealProperty> itemHeal = new List<PlayerItem.HealProperty>();
    public static int limit = 11;
    public static void AddItem(string name, string description, string useDialogue, bool destroyAfterUse = false, int heal = 0)
    {
        if (playerItems.Count < limit)
        {
            PlayerItem item = new PlayerItem
            {
                Name = name,
                Description = description,
                UseDialogue = useDialogue
            };

            PlayerItem.HealProperty healProperty = new PlayerItem.HealProperty
            {
                DestroyAfterUse = destroyAfterUse,
                Heal = heal
            };

            playerItems.Add(item);
            itemHeal.Add(healProperty);
        }
    }

    public static void RemoveItem(int index)
    {
        playerItems.RemoveAt(index);
        itemHeal.RemoveAt(index);
    }

    public static string GetName(int index)
    {
        if (playerItems.Count <= index)
        {
            return "";
        }
        else
        {
            PlayerItem item = playerItems[index];
            return item.Name;
        }
    }

    public static string GetDecription(int index)
    {
        if (playerItems.Count <= index)
        {
            return "";
        }
        else
        {
            PlayerItem item = playerItems[index];
            return item.Description;
        }
    }

    public static string GetUseDialogue(int index)
    {
        if (playerItems.Count <= index)
        {
            return "";
        }
        else
        {
            PlayerItem item = playerItems[index];
            return item.UseDialogue;
        }
    }

    public static int GetHeal(int index)
    {
        if (playerItems.Count <= index)
        {
            return 0;
        }
        else
        {
            PlayerItem.HealProperty healProperty = itemHeal[index];
            return healProperty.Heal;
        }
    }
}
