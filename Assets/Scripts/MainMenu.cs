using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    [SerializeField] private GameObject leaderboardPanel;

    void Start(){
    }

    public void PlayGame(){
        SceneManager.LoadScene("CountdownScene");  
    }

     public void Settings(){
        SceneManager.LoadScene("SettingScene"); 
    }

    public void Leaderboard(){
        leaderboardPanel.SetActive(true);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
