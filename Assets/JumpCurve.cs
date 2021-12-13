using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class JumpCurve : MonoBehaviour
{

    Vector3 initialVel;
    public Vector3[] traj;
    //public Transform trajObj;
    public Transform trajPoint;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = SpawnPlayer.player;
        initialVel = new Vector3(4, 9);

        Debug.Log("here");

        if(player != null)
        {
            traj = Trajectory(10, 0.2f, initialVel);
        }

        //for (int i = 0; i < traj.Length; i++)
        //{
        //    Instantiate(trajObj, trajPoint.position + traj[i], Quaternion.identity);
        //}


        

    }

    Vector3[] Trajectory(int steps, float timeStep, Vector3 initial)
    {
        Vector3[] traj = new Vector3[steps];
        for (int i = 0; i < steps; i++)
        {
            float t = timeStep * i;
            traj[i] = initial * t;
            traj[i] += 0.5f * new Vector3(Physics2D.gravity.x, Physics2D.gravity.y + player.GetComponent<PlayerMovement>().gravity) * (t * t);
        }
        return traj;
    }

    //Update is called once per frame
    void Update()
    {
        for (int i = 0; i < traj.Length - 1; i++)
        {
            Debug.DrawLine(transform.position + traj[i], transform.position + traj[i + 1], Color.red);
        }

    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
            UnityEditor.SceneView.RepaintAll();
        }
    }
}
