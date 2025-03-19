using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour{
    private EnemyUFOSpawner myEnemyUFOSpawner;
    private PowerUpSpawner powerUpSpawner;
    private PowerUp2Spawner powerUp2Spawner;
    private PowerUp3Spawner powerUp3Spawner;
    private ScoreManager myScoreManager;
    private int enemyUFOThisRound = 10;
    private int enemyUFOLeftInRound = 0;
    private int powerUp1Used = 0;
    private int powerUp2Used = 0;
    private int powerUp3Used = 0;
    private int destroyUFOPoints = 25;
    public bool isRoundOver = false;
    public bool isPaused = false;
    public int score = 0;
    public int level = 1;
    public float enemyUFOSpeed = 1f;
    public int lasersLeft = 30;
    public bool isGameOver = false;
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
    [SerializeField] private TextMeshProUGUI inLauncherText;
    [SerializeField] private TextMeshProUGUI powerUp1Text;
    [SerializeField] private TextMeshProUGUI powerUp2Text;
    [SerializeField] private TextMeshProUGUI powerUp3Text;
    [SerializeField] private TextMeshProUGUI beatItText;
    [SerializeField] private TMP_InputField userName;
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
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject invoPanel;
   
    void Start(){
        currentLasersLoaded = 10;
        lasersLeft -= 10;
        ServiceLocator.Register<GameController>(this);
        planetCounter = GameObject.FindObjectsByType<planets>(FindObjectsSortMode.None).Length;
        myEnemyUFOSpawner = GameObject.FindAnyObjectByType<EnemyUFOSpawner>();
        ServiceLocator.Register<EnemyUFOSpawner>(myEnemyUFOSpawner);
        myScoreManager = ScoreManager.Instance;
        ServiceLocator.Register<ScoreManager>(myScoreManager);
        powerUpSpawner = GameObject.FindAnyObjectByType<PowerUpSpawner>();
        if (powerUpSpawner != null) ServiceLocator.Register<PowerUpSpawner>(powerUpSpawner);
        powerUp2Spawner = GameObject.FindAnyObjectByType<PowerUp2Spawner>();
        if (powerUp2Spawner != null) ServiceLocator.Register<PowerUp2Spawner>(powerUp2Spawner);
        powerUp3Spawner = GameObject.FindAnyObjectByType<PowerUp3Spawner>();
        if (powerUp3Spawner != null) ServiceLocator.Register<PowerUp3Spawner>(powerUp3Spawner);
        ShowLowestHighScore();
        UpdateScoreText();
        UpdateLevelText();
        UpdateLasersText();
        UpdateInLauncherText();
        StartRound();
    }

    void Update(){
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.P)){
            TogglePause();
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.I)){
            Invo();
        }
        if (planetCounter <= 1){
            isGameOver = true;
            if (myScoreManager.IsThisHighScore(score)){
                ShowHighScorePanel();
            } else{
                StartCoroutine(DelayedSceneChange("GameOverScene", 2f)); 
            }
        }
        if (enemyUFOLeftInRound <=0 && !isRoundOver && !isGameOver){
            EnemyUFO[] m = GameObject.FindObjectsByType<EnemyUFO>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if (m.Length == 0){
                isRoundOver = true;
                StartCoroutine(EndOfRound());
            }
       }
    }

    public void SaveScore(){
        //Removed the pausing as that made the game not restart next rounds.
        //Time.timeScale = 1;
        //isPaused = false;
        if (!string.IsNullOrEmpty(userName.text)){
            myScoreManager.AddScore(new HighScoreEntry { score = this.score, userName = userName.text });
        }
        SceneManager.LoadScene("GameOverScene");
    }

    public void ShowHighScorePanel(){
        //isPaused = true;
        highScorePanel.SetActive(true);
        //Time.timeScale = 0;
    }

    public void UpdateScoreText(){
        myScoreText.text = "Score: " + score;
    }

    public void UpdateLevelText(){
        myLevelText.text = "Level: " + level; 
    }

    public void UpdateLasersText(){
        myLaserText.text = "Lasers: " + lasersLeft;
        UpdateInLauncherText();
    }

    public void UpdateInLauncherText(){
        inLauncherText.text = "In Launcher: " + currentLasersLoaded;
    }

    public void AddUFOPoints(){
        score += destroyUFOPoints;
        EnemyUFODestroyed();
        UpdateScoreText();
    }

    public void EnemyUFODestroyed(){
        enemyUFOLeftInRound--;
    }

    public void FiredLaser(){
        if (currentLasersLoaded > 0){
            currentLasersLoaded--;
        }
        if (currentLasersLoaded == 0){
            if (lasersLeft >= 10){
            currentLasersLoaded = 10;
            lasersLeft -= 10;
            }else{
            currentLasersLoaded = lasersLeft;
            lasersLeft = 0;
            }
        }
        UpdateLasersText();
    }

    public void LaserLauncherHit(){
        if (lasersLeft >= 10){
            currentLasersLoaded = 10;
            lasersLeft -= 10;
        }else{
            currentLasersLoaded = lasersLeft;
            lasersLeft = 0;
        }
        UpdateLasersText();
        UpdateInLauncherText();
    }

    public void ExtraEnemies(){
        myEnemyUFOSpawner.ufoToSpawnThisRound += 3;  
    }

    public void AddLaserShots(){
        lasersLeft += 5; 
        UpdateLasersText();
    }

     public void ActivateScoreMultiplier(){
        score = (int)(score * 1.2);
    }

    private void StartRound(){
        myEnemyUFOSpawner.ufoToSpawnThisRound = enemyUFOThisRound;
        enemyUFOLeftInRound = enemyUFOThisRound;
        myEnemyUFOSpawner.StartRound();
        powerUpSpawner?.StartRound();
        powerUp2Spawner?.StartRound();
        powerUp3Spawner?.StartRound();
    }

    public IEnumerator EndOfRound(){
        isPaused = true; 
        pausePanel.SetActive(false);
        invoPanel.SetActive(false);
        yield return new WaitForSeconds(3f);
        endOfRoundPanel.SetActive(true);
        int leftOverLaserBonus = (lasersLeft + currentLasersLoaded) * LaserBonusPoints;
        planets[] planets = GameObject.FindObjectsByType<planets>(FindObjectsSortMode.None);
        int leftOverPlanetBonus = (planets.Length - 1) * PlanetsBonusPoints;
        int totalBonus = leftOverLaserBonus + leftOverPlanetBonus;
        if (level >= 3 && level < 6){
            totalBonus *= 2;
        }
        else if (level >= 6 && level < 9){
            totalBonus *= 3;
        }
        else if (level >= 10){
            totalBonus *= 5;
        }
        LeftOverLaserBonusText.text = "Left over Laser Bonus: " + leftOverLaserBonus;
        LeftOverPlanetBonusText.text = "Left over Planet Bonus: " + leftOverPlanetBonus;
        TotalBonusText.text = "Total Bonus: " + totalBonus;
        score += totalBonus;
        UpdateScoreText();
        CountdownText.text = "NEXT ROUND IN: 3!";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "NEXT ROUND IN: 2!";
        yield return new WaitForSeconds(1f);
        CountdownText.text = "NEXT ROUND IN: 1!";
        yield return new WaitForSeconds(1f);
        endOfRoundPanel.SetActive(false);
        isRoundOver = false;
        isPaused = false;
        lasersLeft = 30;
        enemyUFOSpeed *= enemyUFOSpeedMulti;
        currentLasersLoaded = 10;
        lasersLeft -= 10;
        StartRound();
        level++;
        UpdateLevelText();
        UpdateLasersText();
    }
    
    IEnumerator DelayedSceneChange(string sceneName, float delay){
         yield return new WaitForSeconds(delay);
         SceneManager.LoadScene(sceneName);
    }

     void TogglePause(){
        if (endOfRoundPanel.activeSelf) return;
        invoPanel.SetActive(false);
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

     public void ResumeGame(){
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Invo(){
        if (endOfRoundPanel.activeSelf) return;
        pausePanel.SetActive(false);
        isPaused = !isPaused;
        invoPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void IncrementPowerUpUsage(string powerUpType){
        switch (powerUpType){
            case "PowerUp1":
                powerUp1Used++;
                powerUp1Text.text = powerUp1Used.ToString();
                break;
            case "PowerUp2":
                powerUp2Used++;
                powerUp2Text.text = powerUp2Used.ToString();
                break;
            case "PowerUp3":
                powerUp3Used++;
                powerUp3Text.text = powerUp3Used.ToString();
                break;
        }
    }

    void ShowLowestHighScore(){
        List<HighScoreEntry> highScoreEntryList = SaveScoreManager.LoadScores();
        if (highScoreEntryList != null && highScoreEntryList.Count > 0){
            int lowestHighScore = highScoreEntryList[highScoreEntryList.Count - 1].score;
            beatItText.text = "Beat It: " + lowestHighScore;
        }else{
            beatItText.text = "Beat It: 0";
        }
    }
}
