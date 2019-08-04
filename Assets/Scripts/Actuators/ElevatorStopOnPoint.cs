using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorStopOnPoint : Actuator {

    [Tooltip("The point it will move to when its inactive.")]
    public Transform point0;
    [Tooltip("The point it will move to when its active.")]
    public Transform point1;

    public float platformSpeed;

    private int currPointTarget = 0;
    private Vector3 velocity;
    private const int invalidPoint = 2;

    private void Update()
    {
        if (currPointTarget == 0)
        {
            if (Vector2.Distance(transform.position, point0.position) < velocity.magnitude * Time.deltaTime)
            {
                transform.position = point0.position;
                currPointTarget = invalidPoint;
            }
            else
            {
                transform.Translate(velocity * Time.deltaTime);
            }
        }
        else if (currPointTarget == 1)
        {
            if (Vector2.Distance(transform.position, point1.position) < velocity.magnitude * Time.deltaTime)
            {
                transform.position = point1.position;
                currPointTarget = invalidPoint;
            }
            else
            {
                transform.Translate(velocity * Time.deltaTime);
            }
        }
    }

    public override void OnActivate()
    {
        currPointTarget = 1;
        velocity = GetVelocityToTarget();
    }

    public override void OnDeactivate()
    {
        currPointTarget = 0;
        velocity = GetVelocityToTarget();
    }

    private Vector3 GetVelocityToTarget()
    {
        Vector3 dir = Vector3.zero;
        if (currPointTarget == 0)
        {
            dir = point0.position - transform.position;
        }
        else if (currPointTarget == 1)
        {
            dir = point1.position - transform.position;
        }
        dir.Normalize();
        dir *= platformSpeed;
        return dir;
    }
}
