using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSegment : MonoBehaviour
{
    public float distanceRay = 100;
    public Vector2 originatingPoint;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        RenderLaser();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RenderLaser()
    {
        if (Physics2D.Raycast(transform.position, transform.right))
        {
            
        }
    }
}
