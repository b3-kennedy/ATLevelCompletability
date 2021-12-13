using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public float moveX;
    [HideInInspector] public float gravity;
    [SerializeField] float gravityForce;
    public Vector3 vel;
    [SerializeField] Transform groundCheckPoint;
    public float speed;
    public float maxVelocity;
    [SerializeField] float jumpForce;
    [SerializeField] float frictionForce;
    bool jump;
    float jumpTimer;
    public bool enable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //if (GetComponent<AIMove>().enabled)
        //{
        //    enable = false;
        //}
        //else
        //{
        //    enable = true;
        //}
    }

    

    // Update is called once per frame
    void Update()
    {
        Gravity();
        

        //if (GroundCheck())
        //{
        //    Jump();
        //}

        if (jump)
        {
            jumpTimer += Time.deltaTime;
            if(jumpTimer >= 0.1f)
            {
                if (GroundCheck())
                {
                    jumpTimer = 0;
                    jump = false;
                    moveX = 0;
                    rb.velocity = Vector3.zero;
                }
            }

        }
        if (enable)
        {
            Friction();
            
        }
        PlayerInput();
    }

    void Friction()
    {

        if (moveX == 0)
        {
            if (rb.velocity.x < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x + Time.deltaTime * frictionForce, rb.velocity.y);
            }
            else if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x - Time.deltaTime * frictionForce, rb.velocity.y);
            }
        }
    }

    public void SetFrictionForce()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint.position, -groundCheckPoint.up * 1);
        if (hit.collider.tag == "default")
        {
            frictionForce = 20;
        }
        else if(hit.collider.tag == "ice")
        {
            frictionForce = 5;
        }
    }

    public bool GroundCheck()
    {
        Collider2D[] colliders  = Physics2D.OverlapCircleAll(groundCheckPoint.position, 0.2f);

        foreach (var col in colliders)
        {
            if (col.gameObject.GetComponent<FloorPiece>())
            {
                frictionForce = col.gameObject.GetComponent<FloorPiece>().frictionForce;
            }

            if (col.gameObject != gameObject && !col.isTrigger)
            {
                return true;
            }
        }
        return false;
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb.velocity);
        if (enabled)
        {
            float velx = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
            //rb.velocity = new Vector3(moveX * speed * Time.deltaTime, rb.velocity.y - gravity, 0.0f);

            //vel = new Vector3((velx + moveX * speed * Time.deltaTime), rb.velocity.y - gravity, 0.0f);

            if (GetComponent<AIMove>().enabled)
            {
                vel = new Vector3((velx * GetComponent<AIMove>().dir.x) + moveX * speed * Time.deltaTime, rb.velocity.y - gravity, 0.0f);
            }
            else
            {
                vel = new Vector3(velx + moveX * speed * Time.deltaTime, rb.velocity.y - gravity, 0.0f);
            }


            //vel = new Vector3(velx  + moveX * speed * Time.deltaTime, rb.velocity.y - gravity, 0.0f);


            rb.velocity = vel;
        }

        
        
    }

    public void Jump()
    {
        
        jump = true;
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    void PlayerInput()
    {

        moveX = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && GroundCheck())
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }

    void Dash()
    {
        transform.Translate(transform.right * 200 * Time.deltaTime);
    }

    public void Gravity()
    {
        if (!GroundCheck())
        {
            gravity = Mathf.Clamp(gravity, 0, 100);
            gravity += gravityForce * (Time.deltaTime);
        }
        else
        {
            gravity = 0.0f;
        }
        
    }
}
