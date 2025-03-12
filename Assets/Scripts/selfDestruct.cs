using UnityEngine;

public class selfDestruct : MonoBehaviour{

    [SerializeField] private float destroyTime = 1f;
    void Start(){
        Destroy(gameObject, destroyTime);
    }

    void Update(){
    }
}
