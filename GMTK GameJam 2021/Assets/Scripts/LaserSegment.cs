using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSegment : MonoBehaviour
{
    public GameObject laserPrefab;
    public float distanceRay = 100;
    private LineRenderer lineRenderer;
    LayerMask interactableLayers;
    public LaserSegment childLaser;
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
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.right, distanceRay, interactableLayers, -5);
        Vector2 pos = transform.position;
        Vector2 laserOffsetPoint = pos + (hit2D.point - pos) * ((hit2D.point - pos).magnitude - 0.05f) / (hit2D.point - pos).magnitude;
        if (hit2D)
        {
            Draw2DRay(transform.position, hit2D.point);
            // If we have any children lasers
            if (childLaser != null){
                // Make sure we should still have children lasers (i.e. target is reflective or target is portal)
                Reflective reflective = hit2D.collider.gameObject.GetComponent<Reflective>();
                if (reflective) {
                    // Adjust position
                    childLaser.transform.position = laserOffsetPoint;
                    // Adjust rotation
                    Vector2 d = transform.right.normalized;
                    Vector2 n = hit2D.normal.normalized;
                    Vector2 r = d - 2*Vector2.Dot(d, n)*n; // Equation for reflection
                    float angle = Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg;
                    childLaser.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
                // What about portals
                } else if (hit2D.collider.tag == "Portal") {
                    CircleCollider2D circleCast = (CircleCollider2D) hit2D.collider;
                    childLaser.transform.position = laserOffsetPoint + (hit2D.point - pos).normalized * 2.2f * circleCast.radius;
                } else {
                    // Delete child
                    Debug.Log("Destroying for no hittty");
                    Destroy(childLaser.gameObject);
                }

            } else {
                // Instantiate new children
                Reflective reflective = hit2D.collider.gameObject.GetComponent<Reflective>();
                if (reflective) {
                    // Adjust rotation
                    Vector2 d = transform.right.normalized;
                    Vector2 n = hit2D.normal.normalized;
                    Vector2 r = d - 2*Vector2.Dot(d, n)*n; // Equation for reflection
                    float angle = Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg;
                    // spawn child
                    GameObject laserChild = Instantiate(laserPrefab, laserOffsetPoint, Quaternion.Euler(new Vector3(0f, 0f, angle)));
                    laserChild.transform.parent = transform;
                    laserChild.layer = gameObject.layer;
                    childLaser = laserChild.GetComponent<LaserSegment>();
                } else if (hit2D.collider.tag == "Portal") {
                    CircleCollider2D circleCast = (CircleCollider2D) hit2D.collider;
                    Vector2 spawnPoint = laserOffsetPoint + (hit2D.point - pos).normalized * 2.2f * circleCast.radius;
                    GameObject laserChild = Instantiate(laserPrefab, spawnPoint, transform.rotation);
                    laserChild.transform.parent = transform;
                    childLaser = laserChild.GetComponent<LaserSegment>();
                    foreach (Transform child in hit2D.transform.parent)
                    {
                        if (child.gameObject.layer != gameObject.layer)
                        {
                            laserChild.layer = child.gameObject.layer;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            Draw2DRay(transform.position, transform.right * distanceRay);
            if (childLaser != null){
                Destroy(childLaser.gameObject);
            }
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
