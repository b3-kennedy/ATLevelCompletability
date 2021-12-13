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
        switch (type)
        {
            case (NodeType.JUMP):

                if (!GetComponent<JumpCurve>())
                {
                    gameObject.AddComponent<JumpCurve>();
                }
                break;
            
            case (NodeType.COLLECTABLE):

                if (!GetComponent<CollectableNode>())
                {
                    gameObject.AddComponent<CollectableNode>();
                }
                break;

            case (NodeType.END):

                if (!GetComponent<CollectableNode>())
                {
                    gameObject.AddComponent<CollectableNode>();
                }
                break;


        };
    }

}
