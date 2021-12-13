using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPiece : MonoBehaviour
{
    public float frictionForce;
    public float bounciness;
    BoxCollider2D col;

    private void Start()
    {
        PhysicsMaterial2D newMat = new PhysicsMaterial2D();
        col = GetComponent<BoxCollider2D>();
        newMat.bounciness = bounciness;
        col.sharedMaterial = newMat;
    }

}
