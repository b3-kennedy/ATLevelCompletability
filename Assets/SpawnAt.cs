using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnAt : MonoBehaviour
{
    int number;
    public Text numberText;
    GameObject player;
    int maxNodes;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        maxNodes = player.GetComponent<AIMove>().nodeList.Count-1;
    }

    // Update is called once per frame
    void Update()
    {
        number = Mathf.Clamp(number, 0, maxNodes);
    }

    public void Increase()
    {
        number++;
        UpdateUI();
    }

    public void Decrease()
    {
        number--;
        UpdateUI();
    }

    public void UpdateUI()
    {
        numberText.text = number.ToString();
    }

    public void MovePlayer()
    {
        player.GetComponent<AIMove>().nodeNumber = number;
        player.transform.position = player.GetComponent<AIMove>().nodeList[number].transform.position;
    }
}
