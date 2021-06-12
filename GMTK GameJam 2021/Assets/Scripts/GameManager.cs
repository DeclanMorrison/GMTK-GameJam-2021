using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera primeCamera;
    public Camera frozenCamera;
    public Camera overgrowthCamera;

    public SuperPositionState superPositionState = SuperPositionState.TOGETHER;

    public static GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (superPositionState == SuperPositionState.TOGETHER) 
            {
                SplitDimensions();
            }
            else
            {
                JoinDimensions();
            }
        } 
    }

    void SplitDimensions()
    {
        primeCamera.enabled = false;
        frozenCamera.enabled = true;
        overgrowthCamera.enabled = true;
        superPositionState = SuperPositionState.SPLIT;
    }

    void JoinDimensions()
    {
        primeCamera.enabled = true;
        frozenCamera.enabled = false;
        overgrowthCamera.enabled = false;
        superPositionState = SuperPositionState.TOGETHER;
    }
}
