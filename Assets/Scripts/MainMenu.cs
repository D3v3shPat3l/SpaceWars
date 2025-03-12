using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    void Start(){
    }

    public void PlayGame(){
        SceneManager.LoadScene("CountdownScene");  
    }

     public void Settings(){
        SceneManager.LoadScene("SettingScene"); 
    }

    public void Leaderboard(){
        SceneManager.LoadScene("LeaderboardScene"); 
    }

    public void QuitGame(){
        Application.Quit();
    }
}
