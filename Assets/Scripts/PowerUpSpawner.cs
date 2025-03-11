using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private float yPadding = 0.5f;

    private float minX, maxX;
    public int powerUpToSpawnThisRound = 1;
    private GameController myGameController;

    void Awake()
    {
        myGameController = GameObject.FindObjectOfType<GameController>();
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
    }

    public void StartRound()
    {   
        powerUpToSpawnThisRound = 1;
        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnPowerUp()
    {
        while (powerUpToSpawnThisRound > 0)
        {
            float delayBeforeSpawn = Random.Range(0f, 10f); 
            yield return new WaitForSeconds(delayBeforeSpawn);
            float randomX = Random.Range(minX, maxX);
            Instantiate(powerUpPrefab, new Vector3(randomX, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + yPadding, 0), Quaternion.identity);
            powerUpToSpawnThisRound--;
        }
    }
}
