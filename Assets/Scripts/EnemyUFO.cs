using UnityEngine;

public class EnemyUFO : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject explosionPrefab;
    GameObject[] defenders;
    private GameController myGameController;
    Vector3 target;

    void Start()
    {
        myGameController = GameObject.FindObjectOfType<GameController>();
        defenders = GameObject.FindGameObjectsWithTag("Defenders");
        target = defenders[Random.Range(0,defenders.Length)].transform.position;
        speed = myGameController.enemyUFOSpeed;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            UFOExplode();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Defenders")
        {
            myGameController.EnemyUFODestroyed();
            UFOExplode(); 
            if (col.GetComponent<LaserLauncher>() != null) 
            {
                myGameController.LaserLauncherHit();
                return;
            }     
            Destroy(col.gameObject);
     
        }
        else if (col.tag == "Explosions")
        {
            myGameController.AddUFOPoints();
            UFOExplode();  
        }
    }

    private void UFOExplode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
