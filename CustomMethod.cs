
public static class CustomMethod
{
    ///<summary>
    ///Return string on a specific line 
    ///</summary>
    public static string GetLine(string text, int lineNo)
    {
        string[] lines = text.Replace("\r","").Split('\n');
        return lines.Length >= lineNo ? lines[lineNo-1] : null;
    }
    ///<summary>
    ///Return how many lines are there on a given string
    ///</summary>

    public static int ReturnLine(string text)
    {
        string[] lines = text.Replace("\r","").Split('\n');
        return lines.Length;
    }

    public static string getBetween(string strSource, string strStart, string strEnd)
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }

        return "";
    }
}
