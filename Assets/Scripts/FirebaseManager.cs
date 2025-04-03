using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class FirebaseManager : MonoBehaviour{
    private string databaseURL = "https://spacewars-39e64-default-rtdb.firebaseio.com/highScores.json"; 

    public void SaveScores(List<OnlineHighScoreEntry> highScores){
        string json = JsonUtility.ToJson(new OnlineHighScoreListWrapper { highScores = highScores }, true);
        StartCoroutine(UploadScoresToFirebase(json));
    }

    private IEnumerator UploadScoresToFirebase(string json){
        UnityWebRequest request = new UnityWebRequest(databaseURL, "PUT");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            Debug.Log("Scores uploaded successfully!");
        else
            Debug.LogError("Error uploading scores: " + request.error);
    }

    public IEnumerator LoadScoresFromFirebase(System.Action<List<OnlineHighScoreEntry>> callback){
        UnityWebRequest request = UnityWebRequest.Get(databaseURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success){
            string json = request.downloadHandler.text;

            List<OnlineHighScoreEntry> highScoreList = new List<OnlineHighScoreEntry>();
            if (!string.IsNullOrEmpty(json)){
                OnlineHighScoreEntry[] entries = JsonHelper.FromJson<OnlineHighScoreEntry>(json);
                highScoreList.AddRange(entries);
                highScoreList.Sort((a, b) => b.score.CompareTo(a.score));
            }
            callback(highScoreList);
        }else{
            Debug.LogError("Error loading scores: " + request.error);
        }
    }
}

public static class JsonHelper{
    public static T[] FromJson<T>(string json){
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>{
        public T[] array;
    }
}

[System.Serializable]
public class OnlineHighScoreEntry{
    public int score;
    public string userName;
}

[System.Serializable]
public class OnlineHighScoreListWrapper{
    public List<OnlineHighScoreEntry> highScores;
}
