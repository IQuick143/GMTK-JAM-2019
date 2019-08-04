using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Trigger {
    //Leaver on the right -> active (-30 º)
    //Leaver on the left -> inactive (30 º)

    bool movingLever = false;
    private float rightAngle = -30f;
    private float leftAngle = 30f;
    public float turningSpeed = 250f;
    private float rotation = 30;
    private Transform leverModel;

    private void Start() {
        leverModel = transform.Find("Model");
    }

    void Update() {
        if (movingLever) {
            if (active == true) {
                float newRotation = rotation - turningSpeed * Time.deltaTime;
                if (newRotation <= rightAngle) {
                    rotation = rightAngle;
                    movingLever = false;
                }
                else {
                    rotation = newRotation;
                }
            }
            else {
                float newRotation = rotation + turningSpeed * Time.deltaTime;
                if (newRotation >= leftAngle) {
                    rotation = leftAngle;
                    movingLever = false;
                }
                else {
                    rotation = newRotation;
                }
            }
            leverModel.rotation = Quaternion.Euler(0f, 0f, rotation);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            float xVelocity = collision.GetComponent<Rigidbody2D>().velocity.x;
            if (xVelocity > 0f) {
                if (rotation != rightAngle) {
                    //TODO: Play leaver SFX here
                    movingLever = true;
                }
                active = true;
            }
            else if (xVelocity < 0f) {
                if (rotation != leftAngle) {
                    //TODO: Play leaver SFX here
                    movingLever = true;
                }
                active = false;
            }
        }
    }
}
