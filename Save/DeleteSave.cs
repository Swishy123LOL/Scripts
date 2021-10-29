using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSave : MonoBehaviour
{
    public string[] SaveToDelete;
    public string index;
    public int Cutscenes;
    public void Delete()
    {
        for (int o = 0; o < Cutscenes; o++)
        {
            SaveGame.Delete("Cs" + o + index);
        }
        for (int i = 0; i < SaveToDelete.Length; i++)
        {
            SaveGame.Delete(SaveToDelete[i] + index);
            SaveGame.Save<bool>("Saved" + FindObjectOfType<GlobalTime>().GlobalIndex.ToString(), false);
            FindObjectOfType<TimeSaves>().text.text = "0h : 0m";
        }
    }

    public void JustDelete()
    {
        for (int i = 0; i < SaveToDelete.Length; i++)
        {
            SaveGame.Delete(SaveToDelete[i] + index);
            SaveGame.Save<bool>("Saved" + FindObjectOfType<GlobalTime>().GlobalIndex.ToString(), false);
        }
    }

    public void DeleteAll()
    {
        SaveGame.DeleteAll();
    }

}
