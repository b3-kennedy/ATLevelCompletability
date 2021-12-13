using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerControl : MonoBehaviour
{
    public Transform player;
    [SerializeField] float camSpeed;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;
    float defaultSpeed;
    float playerVelY;


    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = camSpeed;
        
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x + player.GetComponent<PlayerMovement>().vel.x* xOffset, player.position.y + yOffset, transform.position.z), Time.deltaTime * camSpeed);
    }
}
