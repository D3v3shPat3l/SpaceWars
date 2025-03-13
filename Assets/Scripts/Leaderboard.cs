using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections; 

public class Leaderboard : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI[] nameTextArray;
    [SerializeField] private TextMeshProUGUI[] scoreTextArray;
    [SerializeField] private AudioClip newEntrySound;
    private AudioSource audioSource;

    
    private Color goldColor = new Color(1f, 0.843f, 0f); 
    private Color greenColor = new Color(0.494f, 0.850f, 0.341f);

    void Start(){
        audioSource = GetComponent<AudioSource>(); 
        audioSource.playOnAwake = false;  
        ShowHighScores();
    }

    public void ShowHighScores(){
        List<HighScoreEntry> highScoreEntryList = SaveScoreManager.LoadScores();
        bool newEntryAdded = false;
        bool scoreUpdated = false;

        string lastLeaderboard = PlayerPrefs.GetString("LastLeaderboard", "");

        string currentLeaderboard = "";
        List<string> currentNames = new List<string>();
        List<int> currentScores = new List<int>();

        if (highScoreEntryList == null || highScoreEntryList.Count == 0){
            for (int i = 0; i < nameTextArray.Length; i++){
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "0";
            }
            return;
        }

        for (int i = 0; i < nameTextArray.Length; i++){
            if (i < highScoreEntryList.Count){
                nameTextArray[i].text = highScoreEntryList[i].userName;
                scoreTextArray[i].text = highScoreEntryList[i].score.ToString();
                currentLeaderboard += highScoreEntryList[i].userName + ":" + highScoreEntryList[i].score + ",";
                currentNames.Add(highScoreEntryList[i].userName);
                currentScores.Add(highScoreEntryList[i].score);

                if (!string.IsNullOrEmpty(lastLeaderboard)){
                    string[] lastEntries = lastLeaderboard.Split(',');
                    bool playerFound = false;
                    foreach (var entry in lastEntries){
                        if (string.IsNullOrEmpty(entry)) continue;
                        string[] entryParts = entry.Split(':');
                        if (entryParts.Length == 2) {
                            string lastPlayerName = entryParts[0];
                            if (lastPlayerName == highScoreEntryList[i].userName){
                                playerFound = true;
                                int previousScore = int.Parse(entryParts[1]);
                                if (highScoreEntryList[i].score != previousScore){
                                    scoreUpdated = true;
                                }
                                break;
                            }
                        }
                    }
                    if (!playerFound){
                        newEntryAdded = true; 
                    }
                }
            } else{
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "0";
            }
        }

        PlayerPrefs.SetString("LastLeaderboard", currentLeaderboard);
        PlayerPrefs.Save();

        if ((newEntryAdded || scoreUpdated) && newEntrySound != null){
            audioSource.playOnAwake = true; 
            audioSource.PlayOneShot(newEntrySound);
            audioSource.playOnAwake = false; 
            StartCoroutine(FlashHighScoreText());
        }
    }

    private IEnumerator FlashHighScoreText(){
        float flashDuration = 6f; 
        float flashInterval = 0.5f; 

        float timeElapsed = 0f;
        bool toggleColor = true;
        while (timeElapsed < flashDuration){
            Color targetColor = toggleColor ? goldColor : greenColor;
            foreach (var nameText in nameTextArray){
                nameText.color = targetColor;
            }
            foreach (var scoreText in scoreTextArray){
                scoreText.color = targetColor;
            }
            toggleColor = !toggleColor; 
            timeElapsed += flashInterval;
            yield return new WaitForSeconds(flashInterval); 
        }
        foreach (var nameText in nameTextArray){
            nameText.color = greenColor;
        }
        foreach (var scoreText in scoreTextArray){
            scoreText.color = greenColor;
        }
    }
}
