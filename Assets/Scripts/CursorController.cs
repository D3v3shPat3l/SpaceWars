using UnityEngine;

public class CursorController : MonoBehaviour
{
   [SerializeField] GameObject missilePrefab;
   [SerializeField] GameObject missileLauncherPrefab;
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
        if (Input.GetMouseButtonDown(0) && myGameController.missilesLeft>0)
        {
            Instantiate(missilePrefab, missileLauncherPrefab.transform.position, Quaternion.identity);
            myGameController.missilesLeft--;
            myGameController.UpdateMissilesText();
        }
    }
}
