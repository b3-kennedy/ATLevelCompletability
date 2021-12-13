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
    public Vector3 dir;
    [HideInInspector]public int nodeNumber;
    bool canMove = true;
    bool inc;
    // Start is called before the first frame update
    void Start()
    {
        nodeNumber = 0;
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        currentNode = nodeList[0];
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
       
        Debug.Log(dir);

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
                    canMove = false;
                    break;
                case (Node.NodeType.END):
                    canMove = false;
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
    }


    void Move()
    {
       
        float velx = Mathf.Clamp(rb.velocity.x, -20, 20);
        rb.velocity = new Vector3((velx + speed * Time.deltaTime) * dir.x, rb.velocity.y, 0.0f);
        //Debug.Log(rb.velocity);
    }
}
