using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour{
   
    public void MenuBack(){
        SceneManager.LoadScene("MenuScene");  
    }
}
