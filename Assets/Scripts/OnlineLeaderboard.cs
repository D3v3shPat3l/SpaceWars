using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class OnlineLeaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] nameTextArray;
    [SerializeField] private TextMeshProUGUI[] scoreTextArray;
    [SerializeField] private GameObject leaderboardOnlinePanel;
    [SerializeField] private GameObject leaderboardPanel;
    private FirebaseManager firebaseManager;

    void Start()
    {
        firebaseManager = Object.FindFirstObjectByType<FirebaseManager>();
        ShowOnlineHighScores();
    }

    public void ShowOnlineHighScores()
    {
        StartCoroutine(firebaseManager.LoadScoresFromFirebase(OnScoresLoaded));
    }

    private void OnScoresLoaded(List<OnlineHighScoreEntry> highScoreEntryList)
    {
        if (highScoreEntryList == null || highScoreEntryList.Count == 0)
        {
            for (int i = 0; i < nameTextArray.Length; i++)
            {
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "0";
            }
            return;
        }

        // Sort and show top entries
        var sortedList = highScoreEntryList
            .OrderByDescending(entry => entry.score)
            .Take(nameTextArray.Length)
            .ToList();

        for (int i = 0; i < nameTextArray.Length; i++)
        {
            if (i < sortedList.Count)
            {
                nameTextArray[i].text = sortedList[i].userName;
                scoreTextArray[i].text = sortedList[i].score.ToString();
            }
            else
            {
                nameTextArray[i].text = "---";
                scoreTextArray[i].text = "0";
            }
        }
    }

    public void SwitchLeaderboard()
    {
        leaderboardOnlinePanel.SetActive(false);
        leaderboardPanel.SetActive(true);
    }
}
