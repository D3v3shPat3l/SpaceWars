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
        AssignButton("Invobtn", OpenInventory);
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
        Debug.Log("Starting Game...");
        SceneManager.LoadScene("CountdownScene");  

    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }

    public void OpenSettings()
    {
        Debug.Log("Opening Settings...");
        SceneManager.LoadScene("SettingScene");  
    }

    public void OpenLeaderboard()
    {
        Debug.Log("Opening Leaderboard...");
        SceneManager.LoadScene("LeaderboardScene");  
    }

    public void OpenInventory()
    {
        Debug.Log("Opening Inventory...");
        SceneManager.LoadScene("InventoryScene");  
    }
}
