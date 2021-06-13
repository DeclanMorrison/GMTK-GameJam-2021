using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSegment : MonoBehaviour
{
    public float distanceRay = 100;
    private LineRenderer lineRenderer;
    LayerMask interactableLayers;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        interactableLayers = (1 << gameObject.layer);
    }

    // Update is called once per frame
    void Update()
    {
        RenderLaser();
    }

    void RenderLaser()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.right, distanceRay, interactableLayers);
        if (hit2D)
        {
            Draw2DRay(transform.position, hit2D.point);
        }
        else
        {
            Draw2DRay(transform.position, transform.right * distanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
