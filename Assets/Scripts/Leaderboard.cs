using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour{
    
    void Start(){
    }

    public void Back(){
        SceneManager.LoadScene("MenuScene");  
    }
}
