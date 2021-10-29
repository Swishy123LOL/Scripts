using System;
using UnityEngine;
using System.Reflection;
using UnityEngine.SceneManagement;

//Various methods that I found helpful, some of them are found online
public static class HelpingHand
{
    public static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType()) return null; // type mis-match
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
        PropertyInfo[] pinfos = type.GetProperties(flags);
        foreach (var pinfo in pinfos) {
            if (pinfo.CanWrite) {
                try {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
            }
        }
        FieldInfo[] finfos = type.GetFields(flags);
        foreach (var finfo in finfos) {
            finfo.SetValue(comp, finfo.GetValue(other));
        }
        return comp as T;
    }

    public static T[] Append<T>(this T[] array, T item)
    {
        if (array == null)
        {
            return new T[] { item };
        }
        T[] result = new T[array.Length + 1];
        array.CopyTo(result, 0);
        result[array.Length] = item;
        return result;
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static string ReturnNameByPath(string path)
    {
        string curr = "";
        for (int i = path.Length - 1; i > -1; i--)
        {
            if (path[i].ToString() == "/")
            {
                break;
            }
            curr += path[i];
        }
        curr = Reverse(curr);
        curr = curr.Replace(".unity", "");
        return curr;
    }

    public static string GetSceneName(int index)
    {
        return ReturnNameByPath(SceneUtility.GetScenePathByBuildIndex(index));
    }

    public static string ConvertTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt(time / 60 % 60);
        int seconds = Mathf.FloorToInt(time % 60);

        string res = (hours < 10) ? "0" + hours.ToString() + ":" : hours.ToString() + ":";
        res += (minutes < 10) ? "0" + minutes.ToString() + ":" : minutes.ToString() + ":";
        res += (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();

        return res;
    }

    public static float VectorAngle(Vector2 vct1, Vector2 vct2, bool radian = false)
    {
        float sign = (vct2.y < vct1.y) ? 1 : -1;
        float angle = Vector2.Angle(vct1, vct2) * sign;

        if (radian) { angle *= Mathf.Deg2Rad; }
        return angle;
    }

    public static bool RandomChanceInt(int n)
    {
        int r = UnityEngine.Random.Range(0, n);
        return r == 0;
    }

    public static bool RandomChanceFloat(float max, float percentage)
    {
        float r = UnityEngine.Random.Range(0, max);
        return r < max * percentage / 100;
    }
}
