using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour{
    private List<HighScoreEntry> highScoreEntryList;
    public static ScoreManager Instance;

    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else{
            Destroy(gameObject);
        }

        highScoreEntryList = SaveScoreManager.LoadScores();
    }

    public void SaveScores(){
        highScoreEntryList.Sort((a, b) => b.score.CompareTo(a.score));
        if (highScoreEntryList.Count > 5){
            highScoreEntryList = highScoreEntryList.GetRange(0, 5);
        }
        SaveScoreManager.SaveScores(highScoreEntryList);
    }

    public bool IsThisHighScore(int score){
        if (highScoreEntryList.Count < 5){
            return true; 
        }
        return score > highScoreEntryList[highScoreEntryList.Count - 1].score;
    }

    /*public void AddScore(HighScoreEntry newScoreEntry){
        highScoreEntryList.Add(newScoreEntry);
        SaveScores();
    }*/

    public void AddScore(HighScoreEntry newScoreEntry){
    HighScoreEntry existingEntry = highScoreEntryList.Find(entry => entry.userName.Equals(newScoreEntry.userName, System.StringComparison.OrdinalIgnoreCase));
    if (existingEntry != null){
        if (newScoreEntry.score > existingEntry.score){
            existingEntry.score = newScoreEntry.score;
        }
    } else {
        highScoreEntryList.Add(newScoreEntry);
    }
    SaveScores();
    }
}
