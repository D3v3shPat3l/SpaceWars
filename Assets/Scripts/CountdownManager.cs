using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownManager : MonoBehaviour{
    public GameObject[] countdownObjects; 
    public float countdownTime = 1f;

    private void Start(){
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine(){
        foreach (GameObject obj in countdownObjects){
            obj.SetActive(false);
        }

        for (int i = 0; i < countdownObjects.Length; i++){
            countdownObjects[i].SetActive(true); 
            yield return new WaitForSeconds(countdownTime);
            if (i < countdownObjects.Length - 1) {
                countdownObjects[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("GameScene");
    }
}
