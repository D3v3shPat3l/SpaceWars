using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class OnlineLeaderboard : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI[] nameTextArray;
    [SerializeField] private TextMeshProUGUI[] scoreTextArray;
    [SerializeField] private GameObject leaderboardOnlinePanel;
    [SerializeField] private GameObject leaderboardPanel;
    private FirebaseManager firebaseManager;

    void Start(){
        firebaseManager = Object.FindFirstObjectByType<FirebaseManager>();
        ShowOnlineHighScores(); 
    }

    public void ShowOnlineHighScores(){
        StartCoroutine(firebaseManager.LoadScoresFromFirebase(OnScoresLoaded));
    }

    private void OnScoresLoaded(List<OnlineHighScoreEntry> highScoreEntryList){
        if (highScoreEntryList == null || highScoreEntryList.Count == 0){
            for (int i = 0; i < nameTextArray.Length; i++){
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "0";
            }
            return;
        }

        string currentLeaderboard = "";

        for (int i = 0; i < nameTextArray.Length; i++){
            if (i < highScoreEntryList.Count){
                nameTextArray[i].text = highScoreEntryList[i].userName;
                scoreTextArray[i].text = highScoreEntryList[i].score.ToString();
                currentLeaderboard += highScoreEntryList[i].userName + ":" + highScoreEntryList[i].score + ",";
            }else{
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "0";
            }
        }
        PlayerPrefs.SetString("LastLeaderboard", currentLeaderboard);
        PlayerPrefs.Save();
    }

    public void SwitchLeaderboard(){
        leaderboardOnlinePanel.SetActive(false);
        leaderboardPanel.SetActive(true);
    }
}
