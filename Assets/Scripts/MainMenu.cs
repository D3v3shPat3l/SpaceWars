using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private GameObject settingPanel;

    void Start(){
        ApplySavedAudioSettings();
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
    
    void ApplySavedAudioSettings(){
        if (Music.instance != null)
            Music.instance.GetComponent<AudioSource>().mute = PlayerPrefs.GetInt("Music", 1) == 0;

        foreach (AudioSource audio in Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
            if (audio != Music.instance?.GetComponent<AudioSource>())
                audio.mute = PlayerPrefs.GetInt("SoundEffects", 1) == 0;
    }
}
