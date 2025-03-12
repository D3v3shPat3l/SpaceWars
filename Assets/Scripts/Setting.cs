using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour{
   
    void Start(){
        
    }

    public void Back(){
        SceneManager.LoadScene("MenuScene");
    }
}
