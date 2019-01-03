using UnityEngine;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

public class LogUtil
{
    public static readonly bool Debugging = true;

    public static void Log(string info)
    {
        if (Debugging)
            Debug.Log(info);
    }

    public static void Error(string error)
    {
        if (Debugging)
            Debug.LogError(error);
    }

}