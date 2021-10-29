using UnityEngine;
using System.IO;

public class CustomSave 
{ 
    public static void Create(string Name, string ExtraPath = "", string FileExtension = ".asevifle")
    {
        string path = Application.dataPath + "/" + ExtraPath + Name + FileExtension;
        FileStream stream = new FileStream(path, FileMode.Create);

        stream.Close();
    }

    public static string ReturnPath(string Name, string ExtraPath = "", string FileExtension = ".asevifle")
    {
        return Application.dataPath + "/" + ExtraPath + Name + FileExtension;
    } 

    public static void Save(string value, string Name, string ExtraPath = "", string FileExtension = ".asevifle")
    {
        string path = Application.dataPath + "/" + ExtraPath + Name + FileExtension;
        if (File.Exists(path) == false)
        {
            Create(Name, ExtraPath, FileExtension);
        }

        File.WriteAllText(path, "");

        StreamWriter streamWriter = new StreamWriter(path, true);
        streamWriter.Write(value);

        streamWriter.Close();
    }

    public static string Load(string Name, string ExtraPath = "", string FileExtension = ".asevifle")
    {
        string path = Application.dataPath + "/" + ExtraPath + Name + FileExtension;
        if (File.Exists(path))
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader streamReader = new StreamReader(fileStream);
            string value = streamReader.ReadToEnd();

            streamReader.Close();
            return value;
        }
        else
        {
            Directory.CreateDirectory(Application.dataPath + "/" + ExtraPath);
            Debug.Log("Save file not found! In " + path);
            return "";
        }     
    }

    public static void Write(string value, string Name, string ExtraPath = "", string FileExtension = ".asevifle")
    {
        string path = Application.dataPath + "/" + ExtraPath + Name + FileExtension;
        if (File.Exists(path) == false)
        {
            Create(Name, ExtraPath, FileExtension);
        }

        StreamWriter streamWriter = new StreamWriter(path, true);
        streamWriter.WriteLine(value);

        streamWriter.Close();
    }
}
