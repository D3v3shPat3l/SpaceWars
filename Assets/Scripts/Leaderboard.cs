using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI[] nameTextArray;
    [SerializeField] private TextMeshProUGUI[] scoreTextArray;
    [SerializeField] private AudioClip newEntrySound; // Sound effect for new leaderboard entry
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
        audioSource.playOnAwake = false;  // Make sure it doesn't play automatically
        ShowHighScores();
    }

    public void ShowHighScores() {
        List<HighScoreEntry> highScoreEntryList = SaveScoreManager.LoadScores();
        bool newEntryAdded = false;
        bool scoreUpdated = false;

        // Load last saved leaderboard data (names and scores)
        string lastLeaderboard = PlayerPrefs.GetString("LastLeaderboard", "");

        // Store current leaderboard names and scores for comparison
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

        // Loop through the current leaderboard and build the string
        for (int i = 0; i < nameTextArray.Length; i++) {
            if (i < highScoreEntryList.Count) {
                nameTextArray[i].text = highScoreEntryList[i].userName;
                scoreTextArray[i].text = highScoreEntryList[i].score.ToString();

                // Build leaderboard string to save in PlayerPrefs
                currentLeaderboard += highScoreEntryList[i].userName + ":" + highScoreEntryList[i].score + ",";
                currentNames.Add(highScoreEntryList[i].userName);
                currentScores.Add(highScoreEntryList[i].score);

                // Check if the player is new or if their score has been updated
                if (!string.IsNullOrEmpty(lastLeaderboard)) {
                    // Compare with last leaderboard to detect new players or score updates
                    string[] lastEntries = lastLeaderboard.Split(',');

                    bool playerFound = false;
                    foreach (var entry in lastEntries) {
                        // Ensure entry is not empty (due to trailing commas, etc.)
                        if (string.IsNullOrEmpty(entry)) continue;

                        string[] entryParts = entry.Split(':');
                        if (entryParts.Length == 2) {
                            string lastPlayerName = entryParts[0];
                            if (lastPlayerName == highScoreEntryList[i].userName) {
                                playerFound = true;
                                int previousScore = int.Parse(entryParts[1]);
                                if (highScoreEntryList[i].score != previousScore) {
                                    scoreUpdated = true; // Score has been updated
                                }
                                break;
                            }
                        }
                    }

                    if (!playerFound) {
                        newEntryAdded = true; // New entry detected
                    }
                }
            } else {
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "0";
            }
        }

        // Save the current leaderboard data for future comparisons
        PlayerPrefs.SetString("LastLeaderboard", currentLeaderboard);
        PlayerPrefs.Save();

        // Play sound only if a truly new name is added or an existing player's score is updated
        if ((newEntryAdded || scoreUpdated) && newEntrySound != null) {
            audioSource.playOnAwake = true; // Enable Play On Awake to trigger sound
            audioSource.PlayOneShot(newEntrySound);
            audioSource.playOnAwake = false; // Disable Play On Awake again after sound plays
        }
    }
}
