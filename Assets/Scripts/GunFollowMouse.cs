using UnityEngine;

public class GunFollowMouse : MonoBehaviour{
    public Transform gun; 
    public Transform pivotPoint; 
    public float minRotation = 0f; 
    public float maxRotation = 90f; 
    public float rotationSpeed = 40f;

    void Update(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 direction = mousePos - pivotPoint.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetAngle = Mathf.Clamp(targetAngle, minRotation, maxRotation);
        float smoothAngle = Mathf.LerpAngle(pivotPoint.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        pivotPoint.rotation = Quaternion.Euler(0, 0, smoothAngle);
    }
}
