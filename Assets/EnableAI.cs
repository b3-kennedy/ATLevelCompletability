using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnableAI : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiavateAI()
    {
        player.GetComponent<AIMove>().enabled = true;
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
