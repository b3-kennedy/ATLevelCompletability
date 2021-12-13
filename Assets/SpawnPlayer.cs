using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnPlayer : MonoBehaviour
{

    public GameObject spawnAt;


    public static GameObject player;

    public Transform platformParent;

    public GameObject[] platforms;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        platforms = new GameObject[platformParent.transform.childCount];
        for (int i = 0; i < platformParent.transform.childCount; i++)
        {
            platforms[i] = platformParent.transform.GetChild(i).gameObject;
        }

        if(spawnAt != null)
        {
            player.transform.position = new Vector3(spawnAt.transform.position.x, spawnAt.transform.position.y + 2);
        }
        else
        {
            //player.transform.position = new Vector3(platforms[0].transform.position.x, platforms[0].transform.position.y + 2);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
