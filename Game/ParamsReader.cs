using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class ParamsReader
{
    private const string SERVICEPATH = "Assets/Resources/ActParams/ServicesParams.txt";
    private const string CONDIPATH = "Assets/Resources/ActParams/ConditionsParams.txt";
    private const string LETTERPATH = "Assets/Resources/ActParams/LetterPiecesParams.txt";
    public static string[] GetServicesParams()
    {
        return Read(SERVICEPATH);
    }

    public static string[] GetConditionsParams()
    {
        return Read(CONDIPATH);
    }

    public static string[] GetLetterPiecesParams()
    {
        return Read(LETTERPATH);
    }

    private static string[] Read(string path)
    {
        var text = string.Empty;
        using (var reader = new StreamReader(path))
            text = reader.ReadToEnd();
        return text.Split('\n')
            .Skip(1)
            .Select(l => l
            .Trim())
            .ToArray();
    }
}
