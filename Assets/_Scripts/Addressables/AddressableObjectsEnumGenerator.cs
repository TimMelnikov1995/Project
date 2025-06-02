#if UNITY_EDITOR

using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class AddressableObjectsEnumGenerator
{
    static readonly string _path = Application.dataPath + "/_Scripts/Addressables";
    static readonly string _className = "AllAddressableObjects";
    static readonly string _tab = "    ";

    static void CreateFile(string text)
    {
        File.WriteAllText(_path + $"/{_className}.cs", text, Encoding.UTF8);
        AssetDatabase.Refresh();
    }

    public static void Add(string newItem)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("public ")
            .Append("enum ")
            .Append(_className)
            .AppendLine()
            .Append("{");

        foreach (AllAddressableObjects item in (AllAddressableObjects[])Enum.GetValues(typeof(AllAddressableObjects)))
        {
            builder.AppendLine()
                .Append(_tab)
                .Append(item.ToString())
                .Append(",");
        }

        builder.AppendLine()
                .Append(_tab)
                .Append(newItem);

        builder.AppendLine()
            .Append("}");

        CreateFile(builder.ToString());
    }
}

#endif