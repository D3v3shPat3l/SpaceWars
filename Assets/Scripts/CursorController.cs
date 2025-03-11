using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject LaserGunPrefab;
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;
    private GameController myGameController;

    void Start()
    {
        myGameController = GameObject.FindAnyObjectByType<GameController>();
        cursorHotspot = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && myGameController.currentLasersLoaded>0)
        {
            Instantiate(laserPrefab, LaserGunPrefab.transform.position, Quaternion.identity);
            myGameController.FiredLaser();
        }
    }
}
