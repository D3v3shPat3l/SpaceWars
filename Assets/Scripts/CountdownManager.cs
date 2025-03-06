using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

//Manages the transitions between 4 images (countdown).
public class CountdownManager : MonoBehaviour
{
    public Image countdownImage;     
    public Sprite[] countdownSprites; 
    public float countdownTime = 1f;  

    private void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        for (int i = 0; i < countdownSprites.Length; i++)
        {
            countdownImage.sprite = countdownSprites[i]; 
            yield return new WaitForSeconds(countdownTime);
        }

        SceneManager.LoadScene("GameScene");
    }
}
