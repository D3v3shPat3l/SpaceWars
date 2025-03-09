using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardpanel;
    void Start()
    {
        AssignButton("Startbtn", PlayGame);
        AssignButton("Exitbtn", QuitGame);
        AssignButton("Settingsbtn", Settings);
        AssignButton("Leaderbtn", Leaderboard);
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

    public void Settings()
    {
        //SceneManager.LoadScene("SettingsScene");  
    }

    public void Leaderboard()
    {
        leaderboardpanel.SetActive(true); 
    }
}
