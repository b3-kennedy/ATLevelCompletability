using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class RaisingPlatforms : MonoBehaviour
{
    public List<GameObject> platforms;
    int currentPlatform;
    // Start is called before the first frame update
    void Start()
    {
        currentPlatform = 1;
        platforms[0].GetComponent<Rising>().startTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPlatform > 0)
        {
            if (platforms[currentPlatform - 1].GetComponent<Rising>().state == Rising.State.DOWN) 
            {
                platforms[currentPlatform].GetComponent<Rising>().startTimer = true;
            }
        }
    }
}