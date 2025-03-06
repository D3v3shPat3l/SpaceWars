using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float yPadding = 0.5f;

    private float minX, maxX;

    public int missileToSpawnThisRound = 3;
    public float delayBetweenMissiles = 0.5f;

    float yValue;

    void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;

        float randomX = Random.Range(minX, maxX);
        yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

       StartCoroutine(SpawnMissiles());
    }

    void Update()
    {

    }

    public IEnumerator SpawnMissiles()
    {
        while (missileToSpawnThisRound > 0)
        {   
            float randomX = Random.Range(minX, maxX);
            Instantiate(missilePrefab, new Vector3(randomX, yValue, 0), Quaternion.identity);
            missileToSpawnThisRound--;
            yield return new WaitForSeconds(delayBetweenMissiles);
        }
    }

}
