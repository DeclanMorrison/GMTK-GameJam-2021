using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedPhysics : MonoBehaviour
{
    public Collider2D primeObject;
    public Collider2D splitObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.superPositionState == SuperPositionState.TOGETHER)
        {
            splitObject.transform.position = primeObject.transform.position;
            primeObject.isTrigger = false;
            splitObject.isTrigger = true;
        }
        else
        {
            primeObject.transform.position = splitObject.transform.position;
            primeObject.isTrigger = true;
            splitObject.isTrigger = false;
        } 
    }
}
