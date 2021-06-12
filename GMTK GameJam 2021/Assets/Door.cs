using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public float travelDistance = 1;
    public float travelSpeed = 1;
    public bool open;
    private Vector2 startPositionLeft;
    private Vector2 startPositionRight;

    void Start()
    {
        startPositionLeft = leftDoor.position;
        startPositionRight = rightDoor.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetState(!open);
        }
        if (open)
        {
            if(startPositionLeft.x - travelDistance < leftDoor.position.x - travelSpeed * Time.deltaTime)
            {
                leftDoor.position = new Vector2(startPositionLeft.x - travelDistance, startPositionLeft.y);
            }
        }
        else
        {
            if (startPositionLeft.x > leftDoor.position.x + travelSpeed * Time.deltaTime)
            {
                leftDoor.position = new Vector2(startPositionLeft.x, startPositionLeft.y);
            }
        }

    }

    void SetState(bool open)
    {
        this.open = open;
    }
}
