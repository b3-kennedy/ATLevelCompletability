using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CollisionEvents : MonoBehaviour
{
    List<Transform> nodes;
    public GameObject keyObj;

    private void Start()
    {
        nodes = GetComponent<AIMove>().nodeList;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("jump") && GetComponent<PlayerMovement>().GroundCheck())
        {
            GetComponent<PlayerMovement>().rb.AddForce(transform.up * 20, ForceMode2D.Impulse);
        }

        if (other.CompareTag("key"))
        {
            keyObj = new GameObject();
            keyObj.name = "key";
            GetComponent<Inventory>().Add(keyObj);
            Destroy(other.gameObject);
        }

        if(other.CompareTag("door") && GetComponent<Inventory>().Check(keyObj))
        {
            Destroy(other.transform.root.gameObject);
        }

        if (other.CompareTag("death") && GetComponent<AIMove>().enabled == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            int nodeNumber = GetComponent<AIMove>().nodeNumber - 1;
            if (nodes[nodeNumber].GetComponent<Node>().type == Node.NodeType.RISING)
            {
                RisingWarning();
            }
            else if(nodes[nodeNumber].GetComponent<Node>().type == Node.NodeType.JUMP)
            {
                JumpWarning();
            }
        }

        if(other.CompareTag("death") && !GetComponent<AIMove>().enabled)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    void JumpWarning()
    {
        int nodeNumber = GetComponent<AIMove>().nodeNumber - 1;
        if(Vector2.Distance(nodes[nodeNumber].position, nodes[nodeNumber+1].position) > 8)
        {
            DisplayMessage("Jump failed between nodes " + nodeNumber.ToString() + " and " + (nodeNumber + 1).ToString() + ", consider decreasing the distance between these platforms");
        }
        

    }

    void RisingWarning()
    {
        int nodeNumber = GetComponent<AIMove>().nodeNumber - 1;
        if (nodes[nodeNumber+1].parent.GetComponent<Rising>().waitTime < 1)
        {
            DisplayMessage("Level is not Completable, the AI fell at node " + (nodeNumber).ToString() + " consider increasing the wait time of the rising platform");
        }
        else if(nodes[nodeNumber+1].parent.GetComponent<Rising>().moveTime > 10)
        {
            DisplayMessage("Level is not Completable, the AI fell at node " + (nodeNumber).ToString() + " consider decreasing the move time of the rising platform");
        }
        
        
    }

    public void DisplayMessage(string text)
    {
        EditorUtility.DisplayDialog("Message", text, "OK");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("finish") && GetComponent<AIMove>().enabled == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            EditorUtility.DisplayDialog("Message", "Level is Completable", "OK");
            Debug.Log("Finished level");
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("move"))
        {
            transform.SetParent(other.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("move"))
        {
            transform.SetParent(null);
        }
    }


}
