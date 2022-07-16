using System;

public class JSONConverter
{

    public static int JSONToInt(JSONObject j, string fName)
    {
        return Convert.ToInt32(j.GetField(fName).ToString());
    }

    public static string JSONToStr(JSONObject j, string fName)
    {
        string s = j.GetField(fName).ToString();
        return s.Substring(1, s.Length - 2);
    }

    public static JSONPackEnditor CreateJSON ()
    {
        return new JSONPackEnditor();
    }

}