using System.IO;
using UnityEditor;
using UnityEngine;

public class HandleTextFile
{
    //[MenuItem("Tools/Write file")]
    static void WriteString(string text)
    {
        string path = "Assets/Savedata/Script.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();

        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        //TextAsset asset = (TextAsset)Resources.Load("test");
    }

    //[MenuItem("Tools/Read file")]
    static void ReadString(string text)
    {
        string path = "Assets/MAIN/Editor/Script.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
    public void Write(string text)
    {
        WriteString(text);
    }

    public void Read(string text)
    {
        ReadString(text);
    }
}
