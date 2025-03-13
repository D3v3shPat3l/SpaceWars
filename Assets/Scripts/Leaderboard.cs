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

        string lastLeaderboard = PlayerPrefs.GetString("LastLeaderboard", "");

        string currentLeaderboard = "";

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
                currentLeaderboard += highScoreEntryList[i].userName + ",";
            } else {
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "0";
            }
        }

        if (!string.IsNullOrEmpty(lastLeaderboard)) {
            string[] lastNames = lastLeaderboard.Split(',');
            string[] currentNames = currentLeaderboard.Split(',');

            foreach (string name in currentNames) {
                if (!string.IsNullOrEmpty(name) && !System.Array.Exists(lastNames, lastName => lastName == name)) {
                    newEntryAdded = true;
                    break; 
                }
            }
        }

        PlayerPrefs.SetString("LastLeaderboard", currentLeaderboard);
        PlayerPrefs.Save();

        if (newEntryAdded && newEntrySound != null) {
            audioSource.playOnAwake = true; 
            audioSource.PlayOneShot(newEntrySound);
            audioSource.playOnAwake = false; 
        }
    }
}
