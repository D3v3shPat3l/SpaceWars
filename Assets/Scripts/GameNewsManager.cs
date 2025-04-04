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
    string url = $"{apiUrl}?key={apiKey}&ordering=-rating&page_size=15";
    using (UnityWebRequest request = UnityWebRequest.Get(url)){
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success){
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Popular Games Data: " + jsonResponse);
            GameNewsResponse newsResponse = JsonUtility.FromJson<GameNewsResponse>(jsonResponse);
            DisplayNews(newsResponse);
        }else{
            Debug.LogError("Failed to fetch popular games: " + request.error);
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
            newsItem.GetComponent<RectTransform>().localScale = Vector3.one;

            TextMeshProUGUI textComponent = newsItem.GetComponentInChildren<TextMeshProUGUI>();
            string platforms = string.Join(", ", game.platforms.ConvertAll(p => p.platform.name));
            string genres = string.Join(", ", game.genres.ConvertAll(g => g.name));
            textComponent.text = $"Name: {game.name}\nGenres: {genres}\nPlatforms: {platforms}";

            Image imageComponent = newsItem.GetComponentInChildren<Image>();

            if (imageComponent != null){
                if (!string.IsNullOrEmpty(game.background_image)){
                    imageComponent.gameObject.SetActive(true);
                    StartCoroutine(LoadImage(game.background_image, imageComponent));
                }else{
                    imageComponent.gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator LoadImage(string imageUrl, Image imageComponent){
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl)){
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success){
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                imageComponent.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }else{
                Debug.LogWarning("Failed to load image: " + request.error);
            }
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
    public string background_image;
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