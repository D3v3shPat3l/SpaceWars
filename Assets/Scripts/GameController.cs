using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int score = 0;
    public int level = 1;
    public int lasersLeft = 30;

    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI myLevelText;
    [SerializeField] private TextMeshProUGUI myLaserText;

    private int destroyUFOPoints = 25;

    void Start()
    {
        UpdateScoreText();
        UpdateLevelText();
        UpdateLasersText();
    }

    void Update()
    {
        
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
        UpdateScoreText();
    }
}
