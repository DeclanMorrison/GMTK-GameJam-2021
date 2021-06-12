using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerTracking : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float zoomFactor = 1;

    Camera cam; 
    float initialCameraSize;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        initialCameraSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerDistance = player1.GetComponent<Transform>().position - player2.GetComponent<Transform>().position;
        Vector3 cameraPosition = (player1.GetComponent<Transform>().position + player2.GetComponent<Transform>().position) / 2;
        cameraPosition.z = -10;
        transform.position = cameraPosition;
        if (zoomFactor * playerDistance.magnitude > initialCameraSize)
        {
            cam.orthographicSize = zoomFactor * playerDistance.magnitude;
        }
    }
}
