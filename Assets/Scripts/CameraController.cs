using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public float panSpeed = 5f;
    public float zoomSpeed = 1f;

    void Update()
    {
        // Camera movement
        Vector3 newPosition = transform.position;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += panSpeed * Time.deltaTime;
        }

        transform.position = newPosition;

        // Camera zoom
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Camera.main.orthographicSize -= zoomSpeed;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Camera.main.orthographicSize += zoomSpeed;
        }
    }
}

