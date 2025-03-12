using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUFOSpawner : MonoBehaviour{
    [SerializeField] private GameObject ufoPrefab;

    private float minX, maxX;

    public int ufoToSpawnThisRound = 3;
    public float delayBetweenUFO = 0.5f;

    float yValue;

    void Awake(){
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;

        float randomX = Random.Range(minX, maxX);
        yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    public void StartRound(){
        StartCoroutine(SpawnUFO());
    }

    public IEnumerator SpawnUFO(){
        while (ufoToSpawnThisRound > 0){   
            float randomX = Random.Range(minX, maxX);
            Instantiate(ufoPrefab, new Vector3(randomX, yValue, 0), Quaternion.identity);
            ufoToSpawnThisRound--;
            yield return new WaitForSeconds(delayBetweenUFO);
        }
    }
}
