using UnityEngine;

public class PlayerLaser : MonoBehaviour{
    private Vector2 target;
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject explosionPrefab;
    void Start(){
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update(){
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == (Vector3)target){
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}