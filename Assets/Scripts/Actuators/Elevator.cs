using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Actuator {

    public Transform [] points;
    public float platformSpeed;

    private bool isActive;
    private int currPointTarget = 0;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (isActive) {
            //If this cycle it will surpass the position of the target point, place it on the target point
            if (Vector2.Distance(transform.position, points[currPointTarget].position) < rb.velocity.magnitude * Time.fixedDeltaTime) {
                transform.position = points[currPointTarget].position;
                currPointTarget++;
                if (currPointTarget == points.Length) {
                    currPointTarget = 0;
                }
                rb.velocity = GetVelocityToTarget();
            }
        }

    }

    public override void OnActivate() {
        isActive = true;
        rb.velocity = GetVelocityToTarget();
    }

    public override void OnDeactivate() {
        isActive = false;
        rb.velocity = Vector2.zero;
    }

    private Vector2 GetVelocityToTarget()
    {
        Vector2 dir = points[currPointTarget].position - transform.position;
        dir.Normalize();
        dir *= platformSpeed;
        return dir;
    }
}
