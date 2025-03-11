using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private TextMeshProUGUI[] highScoreText;

    void Start()
    {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("CountdownScene");  
    }

     public void Settings()
    {
        settingsPanel.SetActive(true); 
    }

    public void Leaderboard()
    {
        leaderboardPanel.SetActive(true); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
