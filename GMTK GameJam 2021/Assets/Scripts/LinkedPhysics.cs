using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedPhysics : MonoBehaviour
{
    public Transform primeObject;
    public Transform splitObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.superPositionState == SuperPositionState.TOGETHER)
        {
            splitObject.position = primeObject.position;
        }
        else
        {
            primeObject.position = splitObject.position;
        } 
    }
}
