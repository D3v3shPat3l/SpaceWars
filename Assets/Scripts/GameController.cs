using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject endOfRoundPanel;
    EnemyUFOSpawner myEnemyUFOSpawner;
    public int score = 0;
    public int level = 1;
    public int lasersLeft = 30;
    private int enemyUFOThisRound = 10;
    private int enemyUFOLeftInRound = 0;

    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI myLevelText;
    [SerializeField] private TextMeshProUGUI myLaserText;

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
        if (enemyUFOLeftInRound <= 0)
        {
            endOfRoundPanel.SetActive(true);
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
}
