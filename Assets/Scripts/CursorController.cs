using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour{
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject LaserGunPrefab;
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;
    private GameController myGameController;

    void Start(){
        StartCoroutine(FindGameController());
    }

    private IEnumerator FindGameController(){
        while (myGameController == null){
            if (ServiceLocator.HasService<GameController>()){
                myGameController = ServiceLocator.Get<GameController>();
            }
            yield return null; 
        }
        cursorHotspot = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    void Update(){
        if (myGameController == null || myGameController.isPaused || myGameController.isRoundOver) return;
        if (Input.GetMouseButtonDown(0) && myGameController.currentLasersLoaded > 0 && !myGameController.isGameOver){
            Instantiate(laserPrefab, LaserGunPrefab.transform.position, Quaternion.identity);
            myGameController.FiredLaser();
        }
    }
}
