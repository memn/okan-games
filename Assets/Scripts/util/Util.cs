using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

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

    public static IEnumerator<UnityWebRequestAsyncOperation> LoadStreamingAsset(UnityAction<string> callback)
    {
        var filePath = Path.Combine(Application.streamingAssetsPath, "questions.json");
        if (filePath.Contains("://") || filePath.Contains(":///"))
            using (var webRequest = UnityWebRequest.Get(filePath))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                if (callback != null)
                    callback(webRequest.downloadHandler.text);
                
            }
        else if (callback != null)
            callback(File.ReadAllText(filePath));
    }
}