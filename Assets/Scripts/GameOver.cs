using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        AssignButton("Menubtn", MenuBack);
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
}
