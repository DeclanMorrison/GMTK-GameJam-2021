using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode grabKey;
    public Collider2D leftCollider;
    public Collider2D rightCollider;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    public string inputX = "P1X";
    public string inputY = "P1Y";
    private bool facingRight = true;
    public float maxSpeed = 5;

    public LayerMask interactableLayers;
    private ContactFilter2D interactableFilter;
    bool isGrabbing = false;
    GameObject grabbedObject;

    public AudioSource walkingSound;
    public AudioSource draggingSound;

    private Vector2 moveDir
    {
        get
        {
            return new Vector2(Mathf.Sign(rb.velocity.x), Mathf.Sign(rb.velocity.x));
        }
    }
    void Start()
    {
        interactableFilter.SetLayerMask(interactableLayers);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure we can only interact with current dimension
        //Grabbing
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

        //Movement
        Vector2 inputs = new Vector2(Input.GetAxisRaw(inputX), Input.GetAxisRaw(inputY)).normalized;
        rb.velocity = inputs * maxSpeed;

        UpdatePlayerFlip(inputs);

        //Animations
        anim.SetBool("isGrabbing", isGrabbing);
        anim.SetBool("isWalking", inputs != Vector2.zero);


        //Sound
        if (isGrabbing && inputs != Vector2.zero) {
            if (draggingSound.isPlaying == false){draggingSound.Play(); Debug.Log("playing dragging");}
        }
        else if (draggingSound.isPlaying == true) { draggingSound.Stop(); }

        if (inputs != Vector2.zero){
            if (walkingSound.isPlaying == false){walkingSound.Play(); Debug.Log("playing dragging");}
        }
        else if (walkingSound.isPlaying == true) { walkingSound.Stop(); }
    }
    void UpdatePlayerFlip(Vector2 inputs)
    {
        if (isGrabbing) { return; }
        if (inputs.x > 0)
        {
            sr.flipX = false;
            facingRight = true;
        }
        else if (inputs.x < 0)
        {
            sr.flipX = true;
            facingRight = false;
        }
    }
    void CheckPickup()
    {
        Collider2D collider = facingRight ? rightCollider : leftCollider;
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

    public void OnHitByLaser()
    {
        GameManager.instance.GlitchToDeath();
    }

}
