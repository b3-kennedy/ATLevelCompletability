using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> points;
    public int pointIndex;
    public float pauseTimer;
    public float maxPauseTime;
    public float speed;
    public bool moveOnPlay;
    public enum State { PAUSE, MOVE };
    public State state;
    Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        pauseTimer = maxPauseTime;

    }

    // Update is called once per frame
    void Update()
    {

        if (pointIndex >= points.Count)
        {
            pointIndex = 0;
        }

        if (state == State.PAUSE)
        {
            pauseTimer -= Time.deltaTime;
        }


        if (Vector3.Distance(transform.position, points[pointIndex].position) <= 0.5f)
        {
            pointIndex += 1;
            state = State.PAUSE;
            
        }

 

        if (pauseTimer <= 0)
        {
            pauseTimer = maxPauseTime;
            state = State.MOVE;
        }

        if (state == State.MOVE)
        {
            dir = -(transform.position - points[pointIndex].position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }
}
