using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField] float speed = 0.6f;
    [SerializeField] float zoomSpeed = 10.0f;
    [SerializeField] float rotateSpeed = 0.1f;

    float maxHeight = 40f;
    float minHeight = 4f;
    float targetOrthoSize;

    Vector2 p1;
    Vector2 p2;

    void Update(){
        if (Time.timeScale == 0f)
        {
            return;
        }

        float scaledSpeed = speed * Camera.main.orthographicSize;
        float scaledRotateSpeed = rotateSpeed * Camera.main.orthographicSize;

        float hsp = scaledSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float vsp = scaledSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        float scrollSpeed = zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        targetOrthoSize -= scrollSpeed;
        targetOrthoSize = Mathf.Clamp(targetOrthoSize, minHeight, maxHeight);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetOrthoSize, Time.deltaTime * zoomSpeed);

        Vector3 lateralMove = hsp * transform.right;
        Vector3 verticalMove = vsp * transform.up;
        Vector3 move = lateralMove + verticalMove;
        transform.position += move;

        getCameraRotation(scaledRotateSpeed);
        MoveCameraOnMouseEdge(scaledSpeed);
    }

    void getCameraRotation(float scaledRotateSpeed ){
        if(Input.GetMouseButtonDown(2)){
            p1 = Input.mousePosition;
        }
        if(Input.GetMouseButton(2)){
            p2 = Input.mousePosition;
            float dx = -(p2 - p1).x * scaledRotateSpeed;
            transform.rotation *= Quaternion.Euler(new Vector3(0, 0, dx));
            p1 = p2;
        }
    }

    void MoveCameraOnMouseEdge(float scaledSpeed){
        float edgeSize = 10f;
        Vector3 direction = new Vector3();

        if (Input.mousePosition.x > Screen.width - edgeSize){
            direction += Vector3.right;
        }
        else if (Input.mousePosition.x < edgeSize){
            direction += Vector3.left;
        }
        if (Input.mousePosition.y > Screen.height - edgeSize){
            direction += Vector3.up;
        }
        else if (Input.mousePosition.y < edgeSize){
            direction += Vector3.down;
        }
        
        direction = transform.rotation * direction;
        transform.position += direction * scaledSpeed * Time.deltaTime;
    }
}