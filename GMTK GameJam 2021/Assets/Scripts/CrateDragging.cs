using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDragging : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode grabKey;
    public Collider2D leftCollider;
    public Collider2D rightCollider;
    PlayerMovement playerMovement;
    
    public LayerMask interactableLayers;
    private ContactFilter2D interactableFilter;
    public bool isGrabbing = false;
    GameObject grabbedObject;
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        interactableFilter.SetLayerMask(interactableLayers);
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure we can only interact with current dimension
        interactableLayers = (1 << gameObject.layer);
        interactableFilter.SetLayerMask(interactableLayers);
        if (!isGrabbing)
        {
            if (Input.GetKeyDown(grabKey))
            {
                CheckPickup();
            }
        }
        else
        {
            if (Input.GetKeyDown(grabKey) || !IsOkayToGrab())
            {
                DropGrabbed();
            }
        }
    }

    void CheckPickup()
    {
        Collider2D collider = playerMovement.facingRight ? rightCollider : leftCollider;
        Collider2D [] collisions = new Collider2D[10];
        int numCollisions = Physics2D.OverlapCollider(collider, interactableFilter, collisions);
        foreach (Collider2D collision in collisions)
        {
            if (collision != null && collision.tag == "Dragable")
            {
                // Accounts for nested interdimensional objects
                if (collision.gameObject.GetComponentInParent<LinkedPhysics>() != null)
                {
                    grabbedObject = collision.gameObject.transform.parent.gameObject;
                    foreach (Rigidbody2D rigidbody2D in grabbedObject.GetComponentsInChildren<Rigidbody2D>())
                    {
                        Destroy(rigidbody2D);
                    }
                }
                else
                {
                    grabbedObject = collision.gameObject;
                    Rigidbody2D rigidbody2D = grabbedObject.GetComponent<Rigidbody2D>();
                    Destroy(rigidbody2D);
                }
                grabbedObject.transform.SetParent(gameObject.transform);
                isGrabbing = true;
                break;
            }
        }
    }

    void DropGrabbed()
    {
        grabbedObject.transform.parent = null;
        if (grabbedObject.GetComponent<LinkedPhysics>() != null)
        {
            foreach (Transform child in grabbedObject.transform) 
            {
                Rigidbody2D rb2d = child.gameObject.AddComponent<Rigidbody2D>();
                rb2d.drag = 25;
                rb2d.freezeRotation = true;
            }
        }
        else
        {
            Rigidbody2D rb2d = grabbedObject.AddComponent<Rigidbody2D>();
            rb2d.drag = 25;
            rb2d.freezeRotation = true;
        }
        grabbedObject = null;
        isGrabbing = false;
    }

    bool IsOkayToGrab()
    {
        // Check if grabbed objects contains at least one object on the same layer
        if (grabbedObject.layer == gameObject.layer)
        {
            return true;
        }
        foreach (Transform child in grabbedObject.transform)
        {
            if (child.gameObject.layer == gameObject.layer)
            {
                return true;
            }
        }
        return false;
    }
}
