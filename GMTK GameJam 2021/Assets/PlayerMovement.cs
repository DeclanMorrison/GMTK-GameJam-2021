using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxSpeed = 5;
    public float accel = 5;
    public float decel = 5;
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
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


        rb.velocity = inputs * maxSpeed;


        
    }                                                  
}                                                      
                                                       