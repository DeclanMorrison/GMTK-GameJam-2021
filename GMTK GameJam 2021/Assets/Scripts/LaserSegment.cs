using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LaserTag
{
    REFLECTION,
    PORTAL,
    NONE
}

public class LaserSegment : MonoBehaviour
{
    public GameObject laserPrefab;
    public float distanceRay = 100;
    private LineRenderer lineRenderer;
    LayerMask interactableLayers;
    public LaserSegment childLaser;
    public LaserTag childOriginator = LaserTag.NONE;
    
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
            // Trigger any lasering effects on other objects
            if (hit2D.collider.tag != "Dragable") // Make sure we don't hit players in a mean way
            {
                hit2D.collider.gameObject.SendMessage("OnHitByLaser", SendMessageOptions.DontRequireReceiver);
            }
            Draw2DRay(transform.position, laserOffsetPoint);
            // If we have any children lasers
            if (childLaser != null){
                // Make sure we should still have children lasers (i.e. target is reflective or target is portal)
                Reflective reflective = hit2D.collider.gameObject.GetComponent<Reflective>();
                if (reflective && childOriginator == LaserTag.REFLECTION) {
                    // Adjust position
                    childLaser.transform.position = laserOffsetPoint;
                    // Adjust rotation
                    Vector2 d = transform.right.normalized;
                    Vector2 n = hit2D.normal.normalized;
                    Vector2 r = Vector2.Reflect(d, n);
                    float angle = Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg;
                    childLaser.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
                // What about portals
                } else if (hit2D.collider.tag == "Portal" && childOriginator == LaserTag.PORTAL) {
                    CircleCollider2D circleCast = (CircleCollider2D) hit2D.collider;
                    childLaser.transform.position = laserOffsetPoint + (hit2D.point - pos).normalized * 2.2f * circleCast.radius;
                } else {
                    // Delete child
                    Debug.Log("Destroying for no hittty");
                    Destroy(childLaser.gameObject);
                    childOriginator = LaserTag.NONE;
                    childLaser = null;
                }

            } else {
                // Instantiate new children
                Reflective reflective = hit2D.collider.gameObject.GetComponent<Reflective>();
                if (reflective) {
                    // Adjust rotation
                    Vector2 d = transform.right.normalized;
                    Vector2 n = hit2D.normal.normalized;
                    Vector2 r = Vector2.Reflect(d, n); // Equation for reflection
                    float angle = Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg;
                    // spawn child
                    Debug.Log("Spawning reflection");
                    GameObject laserChild = Instantiate(laserPrefab, laserOffsetPoint, Quaternion.Euler(new Vector3(0f, 0f, angle)));
                    laserChild.transform.parent = transform;
                    laserChild.layer = gameObject.layer;
                    childLaser = laserChild.GetComponent<LaserSegment>();
                    childOriginator = LaserTag.REFLECTION;
                } else if (hit2D.collider.tag == "Portal") {
                    CircleCollider2D circleCast = (CircleCollider2D) hit2D.collider;
                    Vector2 spawnPoint = laserOffsetPoint + (hit2D.point - pos).normalized * 2.2f * circleCast.radius;
                    Debug.Log("Spawning portal passthrough");
                    GameObject laserChild = Instantiate(laserPrefab, spawnPoint, transform.rotation);
                    laserChild.transform.parent = transform;
                    childLaser = laserChild.GetComponent<LaserSegment>();
                    childOriginator = LaserTag.PORTAL;
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
                Debug.Log("Destroy for other reason");
                Destroy(childLaser.gameObject);
                childOriginator = LaserTag.NONE;
                childLaser = null;
            }
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
