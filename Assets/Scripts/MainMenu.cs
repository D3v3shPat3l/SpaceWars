using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject helpPanel;

    void Start(){
        ApplySavedAudioSettings();
        if (!PlayerPrefs.HasKey("FirstTimeOpened")){
            helpPanel.SetActive(true);  
            PlayerPrefs.SetInt("FirstTimeOpened", 1);  
            PlayerPrefs.Save();  
        }
    }

    void Update(){
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.H)){
            Help();
        }
    }

    public void PlayGame(){
        SceneManager.LoadScene("CountdownScene");  
    }

     public void Settings(){
        helpPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void Leaderboard(){
        helpPanel.SetActive(false);
        settingPanel.SetActive(false);
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

    public void Help(){
        leaderboardPanel.SetActive(false);
        settingPanel.SetActive(false);
        helpPanel.SetActive(!helpPanel.activeSelf);
    }
}
