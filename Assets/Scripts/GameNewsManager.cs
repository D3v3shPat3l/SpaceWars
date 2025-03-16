using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class GameNewsManager : MonoBehaviour{
    private string apiKey = "49daa518305d4d059205ecef0cde4caa"; 
    private string apiUrl = "https://api.rawg.io/api/games"; 
    public GameObject newsItemPrefab;
    public Transform newsContentParent;

    void Start(){
        StartCoroutine(GetLatestGamesNews());
    }

    private IEnumerator GetLatestGamesNews(){
        string url = $"{apiUrl}?key={apiKey}&ordering=-released&page_size=10";
        using (UnityWebRequest request = UnityWebRequest.Get(url)){
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success){
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("News Data: " + jsonResponse);
                GameNewsResponse newsResponse = JsonUtility.FromJson<GameNewsResponse>(jsonResponse);
                DisplayNews(newsResponse);
            }else{
                Debug.LogError("Failed to fetch news: " + request.error);
            }
        }
    }

    private void DisplayNews(GameNewsResponse newsResponse){
        for (int i = newsContentParent.childCount - 1; i >= 0; i--){
            Destroy(newsContentParent.GetChild(i).gameObject);
        }
        foreach (var game in newsResponse.results){
            GameObject newsItem = Instantiate(newsItemPrefab, newsContentParent);
            newsItem.transform.SetParent(newsContentParent, false);
            RectTransform rectTransform = newsItem.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            TextMeshProUGUI textComponent = newsItem.GetComponentInChildren<TextMeshProUGUI>();
            string platforms = string.Join(", ", game.platforms.ConvertAll(p => p.platform.name));
            string genres = string.Join(", ", game.genres.ConvertAll(g => g.name));
            textComponent.text = $"Name: {game.name}\n" +
                                 $"Released: {game.released}\n" +
                                 $"Genres: {genres}\n" +
                                 $"Platforms: {platforms}";
        }
    }
}

[System.Serializable]
public class GameNewsResponse{
    public Game[] results;
}

[System.Serializable]
public class Game{
    public string name;
    public string released;
    public List<Genre> genres;
    public List<PlatformInfo> platforms;
}

[System.Serializable]
public class Genre{
    public string name;
}

[System.Serializable]
public class PlatformInfo{
    public Platform platform;
}

[System.Serializable]
public class Platform{
    public string name;
}