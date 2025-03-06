using UnityEngine;

public class EnemyUFO : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject explosionPrefab;
    GameObject[] defenders;
    Vector3 target;

    void Start()
    {
        defenders = GameObject.FindGameObjectsWithTag("Defenders");
        target = defenders[Random.Range(0,defenders.Length)].transform.position;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Defenders")
        {
            UFOExplode();       
            Destroy(col.gameObject);
     
        }
        else if (col.tag == "Explosions")
        {
            FindAnyObjectByType<GameController>().AddUFOPoints();
            UFOExplode();  
        }
    }

    private void UFOExplode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
