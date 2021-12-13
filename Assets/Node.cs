using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour
{
    public enum NodeType {JUMP, COLLECTABLE, MOVING_PLATFORM, END};
    public NodeType type;

    private void Start()
    {
        if(type == NodeType.JUMP && !GetComponent<JumpCurve>())
        {
            gameObject.AddComponent<JumpCurve>();
        }
        else if(type == NodeType.COLLECTABLE && !GetComponent<CollectableNode>())
        {
            gameObject.AddComponent<CollectableNode>();
        }
    }

}
