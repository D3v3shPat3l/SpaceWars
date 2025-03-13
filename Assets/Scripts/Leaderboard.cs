using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class Leaderboard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI[] nameTextArray;
    [SerializeField] private TextMeshProUGUI[] scoreTextArray;
    [SerializeField] private AudioClip newEntrySound;
    private AudioSource audioSource;

    private Color goldColor = new Color(1f, 0.843f, 0f);
    private Color greenColor = new Color(0.494f, 0.850f, 0.341f);

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        ShowHighScores();
    }

    public void ShowHighScores() {
        List<HighScoreEntry> highScoreEntryList = SaveScoreManager.LoadScores();
        bool newEntryAdded = false;
        bool scoreUpdated = false;
        int flashIndex = -1;  

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
                                    flashIndex = i; 
                                }
                                break;
                            }
                        }
                    }
                    if (!playerFound) {
                        newEntryAdded = true;
                        flashIndex = i; 
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
            audioSource.PlayOneShot(newEntrySound);
            if (flashIndex != -1) {
                StartCoroutine(FlashHighScoreText(flashIndex));
            }
        }
    }

    private IEnumerator FlashHighScoreText(int index) {
        float flashDuration = 6f;
        float flashInterval = 0.5f;

        float timeElapsed = 0f;
        bool toggleColor = true;

        TextMeshProUGUI nameText = nameTextArray[index];
        TextMeshProUGUI scoreText = scoreTextArray[index];

        while (timeElapsed < flashDuration) {
            Color targetColor = toggleColor ? goldColor : greenColor;
            nameText.color = targetColor;
            scoreText.color = targetColor;
            toggleColor = !toggleColor;
            timeElapsed += flashInterval;
            yield return new WaitForSeconds(flashInterval);
        }

        nameText.color = greenColor;
        scoreText.color = greenColor;
    }
}
