using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CollisionEvents : MonoBehaviour
{

    public GameObject keyObj;
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
            EditorUtility.DisplayDialog("Message", "Level is not Completable, The AI fell at node " + (GetComponent<AIMove>().nodeNumber - 1).ToString(), "OK");
        }

        if(other.CompareTag("death") && !GetComponent<AIMove>().enabled)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
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
