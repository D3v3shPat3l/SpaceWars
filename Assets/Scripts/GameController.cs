using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework.Constraints;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject endOfRoundPanel;
    EnemyUFOSpawner myEnemyUFOSpawner;
    public int score = 0;
    public int level = 1;
    public int lasersLeft = 30;
    private int enemyUFOThisRound = 10;
    private int enemyUFOLeftInRound = 0;
    private bool isRoundOver = false;
    [SerializeField] private int LaserBonusPoints = 5;
    [SerializeField] private int PlanetsBonusPoints = 100;

    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI myLevelText;
    [SerializeField] private TextMeshProUGUI myLaserText;
    [SerializeField] private TextMeshProUGUI LeftOverLaserBonusText;
    [SerializeField] private TextMeshProUGUI LeftOverPlanetBonusText;
    [SerializeField] private TextMeshProUGUI TotalBonusText;
    [SerializeField] private TextMeshProUGUI CountdownText;

    private int destroyUFOPoints = 25;

    void Start()
    {
        myEnemyUFOSpawner = GameObject.FindAnyObjectByType<EnemyUFOSpawner>();

        UpdateScoreText();
        UpdateLevelText();
        UpdateLasersText();
        StartRound();
    }

    void Update()
    {
       if (enemyUFOLeftInRound <=0 && !isRoundOver)
       {
        isRoundOver = true;
        StartCoroutine(EndOfRound());
       }
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

    private void StartRound()
    {
        myEnemyUFOSpawner.ufoToSpawnThisRound = enemyUFOThisRound;
        enemyUFOLeftInRound = enemyUFOThisRound;
        myEnemyUFOSpawner.StartRound();
    }

    public IEnumerator EndOfRound()
    {
        yield return new WaitForSeconds(1f);
        endOfRoundPanel.SetActive(true);
        int leftOverLaserBonus = lasersLeft * LaserBonusPoints;

        planets[] planets = GameObject.FindObjectsOfType<planets>();
        int leftOverPlanetBonus = planets.Length * PlanetsBonusPoints;

        int totalBonus = leftOverLaserBonus + leftOverPlanetBonus;

        LeftOverLaserBonusText.text = "Left over Laser Bonus: " + leftOverLaserBonus;
        LeftOverPlanetBonusText.text = "Left over Planet Bonus: " + leftOverPlanetBonus;
        TotalBonusText.text = "Total Bonus: " + totalBonus;

        score += totalBonus;
        UpdateScoreText();

        CountdownText.text = "NEXT ROUND IN: 3";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "NEXT ROUND IN: 2";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "NEXT ROUND IN: 1";
    
        endOfRoundPanel.SetActive(false);

        StartRound();
        level ++;
        UpdateLevelText();
        lasersLeft = 30;
        UpdateLasersText();


    }
}
