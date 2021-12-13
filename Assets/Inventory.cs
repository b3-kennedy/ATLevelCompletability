using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory;

    public void Add(GameObject obj)
    {
        inventory.Add(obj);
    }

    public void Remove(GameObject obj)
    {
        foreach (var item in inventory)
        {
            if(item == obj)
            {
                inventory.Remove(item);
            }
        }
    }

    public bool Check(GameObject obj)
    {
        foreach (var item in inventory)
        {
            if(item == obj)
            {
                return true;
            }
        }
        return false;
    }

}
