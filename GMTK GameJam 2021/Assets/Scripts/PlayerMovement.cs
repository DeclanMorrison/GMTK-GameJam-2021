using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public float maxSpeed = 5;
    public float accel = 5;
    public float decel = 5;
    public string inputX = "P1X";
    public string inputY = "P1Y";
    public bool facingRight = true;
    private Vector2 moveDir
    {
        get
        {
            return new Vector2(Mathf.Sign(rb.velocity.x), Mathf.Sign(rb.velocity.x));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputs = new Vector2(Input.GetAxisRaw(inputX), Input.GetAxisRaw(inputY)).normalized;

        rb.velocity = inputs * maxSpeed;

        if(inputs.x > 0)
        {
            sr.flipX = false;
            facingRight = true;
        }
        else if(inputs.x < 0)
        {
            sr.flipX = true;
            facingRight = false;
        }
    }                                                  
}                                                      
                                                       