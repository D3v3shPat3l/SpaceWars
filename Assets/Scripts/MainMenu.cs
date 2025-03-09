using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardpanel;
    [SerializeField] private GameObject settingspanel;
    void Start()
    {
    
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("CountdownScene");  
    }

    public void QuitGame()
    {
        Application.Quit();
    }

     public void Settings()
    {
        settingspanel.SetActive(true); 
    }

    public void Leaderboard()
    {
        leaderboardpanel.SetActive(true); 
    }
}
