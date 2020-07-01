using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float speed = 0.5f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.orthographicSize > 1)
        {
            mainCamera.orthographicSize -= speed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && mainCamera.orthographicSize < 30)
        {
            mainCamera.orthographicSize += speed;
        }

        if (Input.mousePosition.x > Screen.width * 0.98 && transform.position.x < 10)
        {
            transform.position = transform.position + new Vector3(speed, 0, 0);
        }
        if (Input.mousePosition.y > Screen.height * 0.98 && transform.position.y < 3)
        {
            transform.position = transform.position + new Vector3(0, speed, 0);
        }
        if (Input.mousePosition.x < Screen.width * 0.03 && transform.position.x > -10)
        {
            transform.position = transform.position - new Vector3(speed, 0, 0);
        }
        if (Input.mousePosition.y < Screen.height * 0.03 && transform.position.y > -2)
        {
            transform.position = transform.position - new Vector3(0, speed, 0);
        }

    }
}
