using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class Util : MonoBehaviour
{
    public static void Load<T>(GameObject parent, GameObject prefab, IEnumerable<T> leaderboard,
        UnityAction<GameObject, T> action)
    {
        foreach (var member in leaderboard)
        {
            var memberObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            action(memberObj, member);
            memberObj.transform.SetParent(parent.transform);
            memberObj.transform.localScale = Vector3.one;
        }
    }

    public static void LoadSingle<T>(GameObject parent, GameObject prefab, T member, UnityAction<GameObject, T> action)
    {
        var memberObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        action(memberObj, member);
        memberObj.transform.SetParent(parent.transform);
        memberObj.transform.localScale = Vector3.one;
    }

    public static void ClearChildren(Transform parent)
    {
        foreach (Transform child in parent)
            Destroy(child.gameObject);
    }

    public static void ClearCache(string fileName)
    {
        var saveFilePath = Path.Combine(Application.persistentDataPath, fileName);
        LogUtil.Log("Trying to clear: " + saveFilePath);
        File.Delete(saveFilePath);
        LogUtil.Log("Clean successful.");
    }

    public static IEnumerator<WWW> LoadStreamingAsset(UnityAction<string> callback)
    {
        var filePath = Path.Combine(Application.streamingAssetsPath, "questions.json");
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            var www = new WWW(filePath);
            yield return www;
            if (callback != null && www.bytes.Length != 0)
                callback(Encoding.UTF8.GetString(www.bytes, 3, www.bytes.Length - 3));
        }
        else if (callback != null)
            callback(File.ReadAllText(filePath));
    }
}