using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    public PlayerMovement player;
    public Rigidbody2D rb;
    public List<Transform> nodeList;
    public float speed;
    public Transform currentNode;
    GameObject movePlatform;
    public Vector3 dir;
    public int nodeNumber;
    bool canMove = true;
    bool platform;
    bool rising;
    bool platformWait;
    bool inc;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        currentNode = nodeList[nodeNumber];
    }

    // Update is called once per frame
    void Update()
    {
        //dir = (transform.position - currentNode.transform.position).normalized;

        
    }

    private void FixedUpdate()
    {
        if(currentNode != null)
        {
            dir = -(transform.position - new Vector3(currentNode.position.x, transform.position.y)).normalized;
        }
       

        if (Vector2.Distance(transform.position, currentNode.position) < 1.1f)
        {
            inc = true;

            switch (nodeList[nodeNumber].GetComponent<Node>().type)
            {
                case (Node.NodeType.JUMP):
                    if (player.GroundCheck())
                    {
                        player.Jump();
                    }
                    break;
                case(Node.NodeType.COLLECTABLE):
                    Debug.Log("Collectable");
                    break;
                case (Node.NodeType.MOVING_PLATFORM):
                    movePlatform = nodeList[nodeNumber + 1].transform.parent.gameObject;
                    Debug.Log(movePlatform);
                    if (nodeList[nodeNumber].GetComponent<MovingNode>())
                    {
                        platform = true;
                    }
                    break;
                case (Node.NodeType.END):
                    canMove = false;
                    break;
                case (Node.NodeType.MOVING_PLATFORM_WAIT):
                    platformWait = true;
                    break;
                case (Node.NodeType.RISING):
                    rising = true;
                    break;

            };
            if (inc && nodeNumber + 1 < nodeList.Count)
            {
                nodeNumber++;
                inc = false;
                currentNode = nodeList[nodeNumber];
            }
            
        }
        else
        {
            if (canMove)
            {
                Move();
            }
            
        }

        if (platform)
        {
            MovingPlatform();
        }

        if (platformWait)
        {
            PlatformWait();
        }

        if (rising)
        {
            Rising();
        }


    }

    void Rising()
    {
        Debug.Log(nodeList[nodeNumber].transform.parent);
        if(nodeList[nodeNumber].transform.parent.GetComponent<Rising>().state == global::Rising.State.UP)
        {
            canMove = true;
            player.Jump();
            rising = false;
        }
        else
        {
            canMove = false;
        }
    }

    void PlatformWait()
    {
        canMove = false;
        Debug.Log(nodeList[nodeNumber-1]);
        float dist = Vector2.Distance(nodeList[nodeNumber - 1].position,nodeList[nodeNumber].position);
        Debug.Log(dist);
        if(dist < 7)
        {
            canMove = true;
            player.Jump();
            platformWait = false;
        }
        
    }

    void MovingPlatform()
    {
        float dist = Vector2.Distance(nodeList[nodeNumber].GetComponent<JumpCurve>().traj[0], 
            nodeList[nodeNumber].GetComponent<JumpCurve>().traj[nodeList[nodeNumber].GetComponent<JumpCurve>().traj.Length-1]);
        if(Vector2.Distance(movePlatform.transform.position, transform.position) < dist)
        {
            canMove = true;
            player.Jump();
            platform = false;
        }
        else
        {
            canMove = false;
        }
        
    }
    void Move()
    {
       
        float velx = Mathf.Clamp(rb.velocity.x, -20, 20);
        rb.velocity = new Vector3((velx + speed * Time.deltaTime) * dir.x, rb.velocity.y, 0.0f);
        //Debug.Log(rb.velocity);
    }
}
