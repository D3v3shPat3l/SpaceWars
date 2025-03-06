using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int score = 0;
    public int level = 1;
    public int missilesLeft = 30;

    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI myLevelText;
    [SerializeField] private TextMeshProUGUI myMissileText;

    private int destroyMissilePoints = 25;

    void Start()
    {
        UpdateScoreText();
        UpdateLevelText();
        UpdateMissilesText();
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

    public void UpdateMissilesText()
    {
        myMissileText.text = "Lasers: " + missilesLeft;
    }

    public void AddMissilePoints()
    {
        score += destroyMissilePoints;
        UpdateScoreText();
    }
}
