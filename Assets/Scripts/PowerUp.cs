using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject explosionPrefab;
    private GameController myGameController;
    private Vector3 target;
    private GameObject[] defenders;

    void Start()
    {
        myGameController = GameObject.FindObjectOfType<GameController>();
        defenders = GameObject.FindGameObjectsWithTag("Defenders");

        if (defenders.Length > 0)
        {
            target = defenders[Random.Range(0, defenders.Length)].transform.position;
        }
        else
        {
            Destroy(gameObject); 
            return;
        }

        speed = myGameController.enemyUFOSpeed;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Defenders"))
        {
            return; 
        }
        else if (col.CompareTag("Explosions"))
        {
            ActivatePowerUp();
            Destroy(col.gameObject); 
        }
    }

    private void ActivatePowerUp()
    {
        myGameController.ExtraEnemies(); 
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
