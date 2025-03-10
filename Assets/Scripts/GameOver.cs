using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardbtnpanel;
    void Start()
    {

    }

    public void MenuBack()
    {
        SceneManager.LoadScene("MenuScene");  
    }
}
