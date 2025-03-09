using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardbtnpanel;
    void Start()
    {
        AssignButton("Menubtn", MenuBack);
        AssignButton("Addhighscorebtn", HighScorePanel);
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

    public void MenuBack()
    {
        SceneManager.LoadScene("MenuScene");  
    }

    public void HighScorePanel()
    {
        leaderboardbtnpanel.SetActive(true);
    }
}
