using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaver
{
    public static void SaveFileJson<T>(T obj, string path)
    {
        File.WriteAllText(path, JsonUtility.ToJson(obj));
    }

    public static T LoadingJson<T>(string path)
    {
        if (File.Exists(path))
            return JsonUtility.FromJson<T>(File.ReadAllText(path));
        else return default(T);
    }
}
