using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI[] nameTextArray;
    [SerializeField] private TextMeshProUGUI[] scoreTextArray;
    [SerializeField] private AudioClip newEntrySound; 
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>(); 
        audioSource.playOnAwake = false;  
        ShowHighScores();
    }

    public void ShowHighScores() {
        List<HighScoreEntry> highScoreEntryList = SaveScoreManager.LoadScores();
        bool newEntryAdded = false;
        bool scoreUpdated = false;

        string lastLeaderboard = PlayerPrefs.GetString("LastLeaderboard", "");

        string currentLeaderboard = "";
        List<string> currentNames = new List<string>();
        List<int> currentScores = new List<int>();

        if (highScoreEntryList == null || highScoreEntryList.Count == 0) {
            for (int i = 0; i < nameTextArray.Length; i++) {
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "0";
            }
            return;
        }

        for (int i = 0; i < nameTextArray.Length; i++) {
            if (i < highScoreEntryList.Count) {
                nameTextArray[i].text = highScoreEntryList[i].userName;
                scoreTextArray[i].text = highScoreEntryList[i].score.ToString();

                currentLeaderboard += highScoreEntryList[i].userName + ":" + highScoreEntryList[i].score + ",";
                currentNames.Add(highScoreEntryList[i].userName);
                currentScores.Add(highScoreEntryList[i].score);

                if (!string.IsNullOrEmpty(lastLeaderboard)) {
                    string[] lastEntries = lastLeaderboard.Split(',');

                    bool playerFound = false;
                    foreach (var entry in lastEntries) {
                        if (string.IsNullOrEmpty(entry)) continue;
                        string[] entryParts = entry.Split(':');
                        if (entryParts.Length == 2) {
                            string lastPlayerName = entryParts[0];
                            if (lastPlayerName == highScoreEntryList[i].userName) {
                                playerFound = true;
                                int previousScore = int.Parse(entryParts[1]);
                                if (highScoreEntryList[i].score != previousScore) {
                                    scoreUpdated = true;
                                }
                                break;
                            }
                        }
                    }

                    if (!playerFound) {
                        newEntryAdded = true; 
                    }
                }
            } else {
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "0";
            }
        }

        PlayerPrefs.SetString("LastLeaderboard", currentLeaderboard);
        PlayerPrefs.Save();

        if ((newEntryAdded || scoreUpdated) && newEntrySound != null) {
            audioSource.playOnAwake = true; 
            audioSource.PlayOneShot(newEntrySound);
            audioSource.playOnAwake = false; 
        }
    }
}
