using System.Collections;
using UnityEngine;

public class PowerUp2Spawner : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private float yPadding = 0.5f;

    private float minX, maxX;
    private GameController myGameController;
    
    void Awake()
    {
        myGameController = GameObject.FindObjectOfType<GameController>();
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
    }

    void Update()
    {

    }

    public void StartRound()
    {
        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnPowerUp()
    {
        float randomX = Random.Range(minX, maxX);
        Instantiate(powerUpPrefab, new Vector3(randomX, Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + yPadding, 0), Quaternion.identity);

        yield return new WaitForSeconds(10f); 
    }
}
