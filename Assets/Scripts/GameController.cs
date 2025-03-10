using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    EnemyUFOSpawner myEnemyUFOSpawner;
    public int score = 0;
    public int level = 1;
    public float enemyUFOSpeed = 1f;
    public int lasersLeft = 30;
    private int enemyUFOThisRound = 10;
    private int enemyUFOLeftInRound = 0;
    private bool isRoundOver = false;
     private int destroyUFOPoints = 25;
    public int currentLasersLoaded = 0;
    public int planetCounter = 0;
    [SerializeField] private int LaserBonusPoints = 5;
    [SerializeField] private int PlanetsBonusPoints = 100;
    [SerializeField] private float enemyUFOSpeedMulti = 2f;
    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI myLevelText;
    [SerializeField] private TextMeshProUGUI myLaserText;
    [SerializeField] private TextMeshProUGUI LeftOverLaserBonusText;
    [SerializeField] private TextMeshProUGUI LeftOverPlanetBonusText;
    [SerializeField] private TextMeshProUGUI TotalBonusText;
    [SerializeField] private TextMeshProUGUI CountdownText;
    [SerializeField] private GameObject endOfRoundPanel;
    [SerializeField] private GameObject earthLeft;
    [SerializeField] private GameObject earthMiddle;
    [SerializeField] private GameObject earthRight;
    [SerializeField] private GameObject marsLeft;
    [SerializeField] private GameObject marsMiddle;
    [SerializeField] private GameObject marsRight;
    [SerializeField] private GameObject nepLeft;
    [SerializeField] private GameObject nepMiddle;
    [SerializeField] private GameObject nepRight;
    [SerializeField] private TextMeshProUGUI inLauncherText;
    
    void Start()
    {
        currentLasersLoaded = 10;
        lasersLeft -= 10;

        myEnemyUFOSpawner = GameObject.FindAnyObjectByType<EnemyUFOSpawner>();
        planetCounter = GameObject.FindObjectsOfType<planets>().Length;

        UpdateScoreText();
        UpdateLevelText();
        UpdateLasersText();
        UpdateInLauncherText();
        StartRound();
    }

    void Update()
    {
       if (enemyUFOLeftInRound <=0 && !isRoundOver)
       {
        isRoundOver = true;
        StartCoroutine(EndOfRound());
       }
       if (planetCounter <= 1)
       {
        StartCoroutine(DelayedSceneChange("GameOverScene", 2f)); 
       }
    }

     IEnumerator DelayedSceneChange(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateScoreText()
    {
        myScoreText.text = "Score: " + score;
    }

    public void UpdateLevelText()
    {
        myLevelText.text = "Level: " + level; 
    }

    public void UpdateLasersText()
    {
        myLaserText.text = "Lasers: " + lasersLeft;
        UpdateInLauncherText();
    }

    public void UpdateInLauncherText()
    {
        inLauncherText.text = "In Launcher: " + currentLasersLoaded;
    }

    public void AddUFOPoints()
    {
        score += destroyUFOPoints;
        EnemyUFODestroyed();
        UpdateScoreText();
    }

    public void EnemyUFODestroyed()
    {
        enemyUFOLeftInRound--;
    }

    public void FiredLaser()
    {
        if (currentLasersLoaded > 0)
        {
            currentLasersLoaded--;
        }
        if (currentLasersLoaded == 0)
        {
            if (lasersLeft >= 10)
            {
            currentLasersLoaded = 10;
            lasersLeft -= 10;
            }
            else
            {
            currentLasersLoaded = lasersLeft;
            lasersLeft = 0;
            }
        }
        UpdateLasersText();
    }

    public void LaserLauncherHit()
    {
        if (lasersLeft >= 10)
        {
            currentLasersLoaded = 10;
            lasersLeft -= 10;
        }
        else
        {
            currentLasersLoaded = lasersLeft;
            lasersLeft = 0;
        }
        UpdateLasersText();
        UpdateInLauncherText();
    }

    private void StartRound()
    {
        myEnemyUFOSpawner.ufoToSpawnThisRound = enemyUFOThisRound;
        enemyUFOLeftInRound = enemyUFOThisRound;
        myEnemyUFOSpawner.StartRound();
    }

    public IEnumerator EndOfRound()
    {
        yield return new WaitForSeconds(3f);
        endOfRoundPanel.SetActive(true);
        int leftOverLaserBonus = (lasersLeft + currentLasersLoaded) * LaserBonusPoints;

        planets[] planets = GameObject.FindObjectsOfType<planets>();
        int leftOverPlanetBonus = (planets.Length - 1) * PlanetsBonusPoints;

        int totalBonus = leftOverLaserBonus + leftOverPlanetBonus;

        if (level >= 3 && level < 6)
        {
            totalBonus *= 2;
        }
        else if (level >= 6 && level < 9)
        {
            totalBonus *= 3;
        }
        else if (level >= 10)
        {
            totalBonus *= 5;
        }

        LeftOverLaserBonusText.text = "Left over Laser Bonus: " + leftOverLaserBonus;
        LeftOverPlanetBonusText.text = "Left over Planet Bonus: " + leftOverPlanetBonus;
        TotalBonusText.text = "Total Bonus: " + totalBonus;

        score += totalBonus;
        UpdateScoreText();

        if (score >= 8000)
        {
        earthLeft.SetActive(false);
        earthMiddle.SetActive(false);
        earthRight.SetActive(false);
        marsLeft.SetActive(true);
        marsMiddle.SetActive(true);
        marsRight.SetActive(true);
        }
        if (score >= 16000)
        {
        marsLeft.SetActive(false);
        marsMiddle.SetActive(false);
        marsRight.SetActive(false);
        nepLeft.SetActive(true);
        nepMiddle.SetActive(true);
        nepRight.SetActive(true);
        }
        
        CountdownText.text = "NEXT ROUND IN: 3";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "NEXT ROUND IN: 2";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "NEXT ROUND IN: 1";
        yield return new WaitForSeconds(1f);
    
        endOfRoundPanel.SetActive(false);
        isRoundOver = false;
        lasersLeft = 30;
        enemyUFOSpeed *= enemyUFOSpeedMulti;
        currentLasersLoaded = 10;
        lasersLeft -= 10;

        StartRound();
        level++;
        UpdateLevelText();
        UpdateLasersText();
    }
}
