using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnableAI : MonoBehaviour
{

    public GameObject player;
    float smallestDist = 9999;
    int nearestNode;
    int index;
    List<Transform> nodes;

    // Start is called before the first frame update
    void Start()
    {
        nodes = player.GetComponent<AIMove>().nodeList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiavateAI()
    {
        GetClosestNode();
        player.GetComponent<AIMove>().enabled = true;
        smallestDist = 9999;
        index = 0;

    }

    void GetClosestNode()
    {
        
        foreach (var node in nodes)
        {
            if(node != null)
            {
                float dist = Vector2.Distance(player.transform.position, node.position);
                if (dist < smallestDist)
                {
                    smallestDist = dist;
                    player.GetComponent<AIMove>().nodeNumber = index;
                    player.GetComponent<AIMove>().currentNode = node;
                }
                index++;
            }
        }
    }

    public void DisableAI()
    {
        player.GetComponent<AIMove>().enabled = false;
    }

    public void ResetPlayer(Transform position)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
