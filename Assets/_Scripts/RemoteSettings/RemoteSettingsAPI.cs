using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class RemoteSettingsAPI
{
    public static void LoadRemoteSettings(RemoteSettings remoteSettings)
    {
        LoadFromGoogle(remoteSettings.Id, remoteSettings.GridId, OnSettingsLoaded, remoteSettings);
    }

    static void LoadFromGoogle(string id, string gridId, Action<string, RemoteSettings> callBack, RemoteSettings remoteSettings)
    {
        string url = $@"https://docs.google.com/spreadsheet/ccc?key={id}&usp=sharing&output=csv&id=KEY&gid={gridId}";

        WWW w = new WWW(url);
        while (!w.isDone)
            w.MoveNext();

        callBack(w.text, remoteSettings);
    }

    static void OnSettingsLoaded(string text, RemoteSettings remoteSettings)
    {
        Dictionary<string, string> textData = new Dictionary<string, string>();

        string[] lines = text.Split("\n");
        foreach (string line in lines)
        {
            string[] columns = line.Split(",");
            string[] nameAndValue = columns.Take(2).Select(p => p.Trim()).ToArray();
            textData.Add(nameAndValue[0], nameAndValue[1]);
        }

        FieldInfo[] fields = remoteSettings.GetType().GetFields();

        foreach(FieldInfo field in fields)
        {
            if(textData.TryGetValue(field.Name, out string textValue))
            {
                switch(field.FieldType)
                {
                        case Type t when t == typeof(int):
                        if (int.TryParse(textValue, out int intValue))
                            field.SetValue(remoteSettings, intValue);
                        break;

                        case Type t when t == typeof(float):
                        if (float.TryParse(textValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float floatValue))
                            field.SetValue(remoteSettings, floatValue);
                        break;
                    }
            }
        }
    }
}