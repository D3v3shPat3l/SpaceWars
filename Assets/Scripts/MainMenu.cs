using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        AssignButton("Startbtn", PlayGame);
        AssignButton("Exitbtn", QuitGame);
        AssignButton("Settingsbtn", OpenSettings);
        AssignButton("Leaderbtn", OpenLeaderboard);
    }

    void AssignButton(string buttonName, UnityEngine.Events.UnityAction action)
    {
        GameObject buttonObj = GameObject.Find(buttonName);
        if (buttonObj)
        {
            Button button = buttonObj.GetComponent<Button>();
            if (button)
            {
                button.onClick.AddListener(action);
            }
            else
            {
                Debug.LogError("No Button component found on " + buttonName);
            }
        }
        else
        {
            Debug.LogError("Button " + buttonName + " not found!");
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("CountdownScene");  
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");  
    }

    public void OpenLeaderboard()
    {
        SceneManager.LoadScene("LeaderboardScene");  
    }
}
