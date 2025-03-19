using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class SaveScoreManager{
    private static string path = Application.persistentDataPath + "/HighScores.json";

    public static void SaveScores(List<HighScoreEntry> highScoreList){
        string json = JsonUtility.ToJson(new HighScoreData(highScoreList), true);
        File.WriteAllText(path, json);
    }

    public static List<HighScoreEntry> LoadScores(){
        if (File.Exists(path)){
            string json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            return data.highScores ?? new List<HighScoreEntry>();
        }
        return CreateDefaultScores();
    }
    
    private static List<HighScoreEntry> CreateDefaultScores(){
    List<HighScoreEntry> defaultScores = new List<HighScoreEntry>{
        new HighScoreEntry { userName = "---", score = 0 },
        new HighScoreEntry { userName = "---", score = 0 },
        new HighScoreEntry { userName = "---", score = 0 },
        new HighScoreEntry { userName = "---", score = 0 },
        new HighScoreEntry { userName = "---", score = 0 }
        };
        SaveScores(defaultScores); 
        return defaultScores;
    }
}

[System.Serializable]
public class HighScoreData{
    public List<HighScoreEntry> highScores;
    public HighScoreData(List<HighScoreEntry> scores){
        highScores = scores;
    }
}

[System.Serializable]
public class HighScoreEntry{
    public int score;
    public string userName;
}
