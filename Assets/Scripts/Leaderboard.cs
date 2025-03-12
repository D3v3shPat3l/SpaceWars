using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI[] nameTextArray;
    [SerializeField] private TextMeshProUGUI[] scoreTextArray;

    void Start(){
        ShowHighScores();
    }

    public void Back(){
        SceneManager.LoadScene("MenuScene");
    }

    public void ShowHighScores(){
        List<HighScoreEntry> highScoreEntryList = SaveScoreManager.LoadScores();
        if (highScoreEntryList == null || highScoreEntryList.Count == 0){
            for (int i = 0; i < nameTextArray.Length; i++){
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "---";
            }
            return;
        }
        for (int i = 0; i < nameTextArray.Length; i++){
            if (i < highScoreEntryList.Count){
                nameTextArray[i].text = highScoreEntryList[i].userName;
                scoreTextArray[i].text = highScoreEntryList[i].score.ToString();
            } else{
            
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "---";
            }
        }
    }
}
