using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Actuator {

    public Transform [] points;
    public float platformSpeed;

    private bool isActive;
    private int currPointTarget = 0;
    private Vector3 velocity;

    private void Update() {
        if (isActive) {
            //If this cycle it will surpass the position of the target point, place it on the target point
            if (Vector2.Distance(transform.position, points[currPointTarget].position) < velocity.magnitude * Time.deltaTime) {
                transform.position = points[currPointTarget].position;
                currPointTarget++;
                if (currPointTarget == points.Length) {
                    currPointTarget = 0;
                }
                velocity = GetVelocityToTarget();
            }
            else
            {
                transform.Translate(velocity * Time.deltaTime);
            }
        }

    }

    public override void OnActivate() {
        isActive = true;
        velocity = GetVelocityToTarget();
    }

    public override void OnDeactivate() {
        isActive = false;
        velocity = Vector2.zero;
    }

    private Vector3 GetVelocityToTarget() {
        Vector3 dir = points[currPointTarget].position - transform.position;
        dir.Normalize();
        dir *= platformSpeed;
        return dir;
    }
}
