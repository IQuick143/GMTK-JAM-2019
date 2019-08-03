using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalForceField : MonoBehaviour {

    public float ySpeed;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D colRb = collision.attachedRigidbody;
        if(colRb != null)
        {
            //TODO: Add an acceleration so it feels better
            colRb.velocity = new Vector3(colRb.velocity.x, ySpeed);
        }
    }
}
