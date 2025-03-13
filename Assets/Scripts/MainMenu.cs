using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private GameObject settingPanel;

    void Start(){
    }

    public void PlayGame(){
        SceneManager.LoadScene("CountdownScene");  
    }

     public void Settings(){
        settingPanel.SetActive(true);
    }

    public void Leaderboard(){
        leaderboardPanel.SetActive(true);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
