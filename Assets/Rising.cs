using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Raising
{
    [HideInInspector]
    public GameObject platform;
    public Vector3 startPos;
    public Vector3 endPos;
}

public class Rising : MonoBehaviour
{
    public Raising platform;
    public float waitTime;
    float waitTimer;
    public float moveTime;
    public bool startTimer;
    public enum State { UP, DOWN, WAIT };
    public State state;
    int currentPlatform;
    State prevState;
    // Start is called before the first frame update
    void Start()
    {
        platform.platform = gameObject;
        platform.startPos = transform.position;
        platform.endPos = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
        waitTimer = waitTime;
        currentPlatform = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            waitTimer -= Time.deltaTime;
        }

        if (waitTimer <= 0)
        {
            switch (prevState)
            {
                case (State.DOWN):
                    state = State.UP;
                    break;
                case (State.UP):
                    state = State.DOWN;
                    break;
            }
        }

        if (state == State.DOWN)
        {
            platform.platform.transform.position = new Vector3(platform.platform.transform.position.x, platform.platform.transform.position.y - moveTime * Time.deltaTime, platform.platform.transform.position.z);
        }

        if (state == State.UP)
        {
            platform.platform.transform.position = new Vector3(platform.platform.transform.position.x, platform.platform.transform.position.y + moveTime * Time.deltaTime, platform.platform.transform.position.z);
        }

        if (Vector2.Distance(platform.platform.transform.position, platform.endPos) < 0.1f && state == State.DOWN)
        {
            waitTimer = waitTime;
            prevState = state;
            state = State.WAIT;
        }

        if (Vector2.Distance(platform.platform.transform.position, platform.startPos) < 0.1f && state == State.UP)
        {
            waitTimer = waitTime;
            prevState = state;
            state = State.WAIT;
        }

    }
}


