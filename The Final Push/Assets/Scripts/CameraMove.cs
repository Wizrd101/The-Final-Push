using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Rigidbody2D rb;
    Camera cam;

    public float speed = 3.0f;

    public int zoomState = 5;
    public int minZoom = 2;
    public int maxZoom = 10;

    public bool lockCam = false;
    public int lockZoomState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Camera>();
        cam.orthographicSize = zoomState;
        if (lockZoomState == 0)
        {
            lockZoomState = 5;
        }
    }

    void Update()
    {
        if (!lockCam)
        {
            // Camera movement, basically copied and pasted from a top-down player movement
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");

            Vector2 moveDirection = new Vector2(xInput, yInput);

            rb.velocity = moveDirection * speed;

            // Handles the zooming in and out of the camera. Q is zoom in, E is zoom out
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (zoomState != maxZoom)
                {
                    zoomState++;
                    cam.orthographicSize = zoomState;
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (zoomState != minZoom)
                {
                    zoomState--;
                    cam.orthographicSize = zoomState;
                }
            }
        }
    }
}
