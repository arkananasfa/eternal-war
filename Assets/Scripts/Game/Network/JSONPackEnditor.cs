using System;
using SocketIO;

public class JSONPackEnditor
{

    private JSONObject json;

    public JSONPackEnditor ()
    {
        json = new JSONObject(JSONObject.Type.OBJECT);
    }

    public JSONPackEnditor Add (string name, int val)
    {
        json.AddField(name, val);
        return this;
    }

    public JSONPackEnditor Add(string name, string val)
    {
        json.AddField(name, val);
        return this;
    }

    public JSONObject Get()
    {
        return json;
    }

}