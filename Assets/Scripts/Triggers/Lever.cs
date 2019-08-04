using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Trigger {
	//Leaver on the right -> active (-30 º)
	//Leaver on the left -> inactive (30 º)

	[SerializeField]
	private bool flipLever = false;

	bool movingLever = false;
	private float rightAngle = -30f;
	private float leftAngle = 30f;
	[SerializeField]
	private float turningSpeed = 250f;
	private float rotation = 30;
	private Transform leverModel;

	private void Start() {
		leverModel = transform.Find("Model");
		if (flipLever) {
			this.rotation = this.rightAngle;
		} else {
			this.rotation = this.leftAngle;
		}
		leverModel.rotation = Quaternion.Euler(0f, 0f, rotation);
	}

	void Update() {
		if (movingLever) {
			if (active != flipLever) {
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
			if (xVelocity != 0f) {
				if (xVelocity > 0f) {
					if (rotation != rightAngle) {
						//TODO: Play lever SFX here
						movingLever = true;
					}
					active = !flipLever;
				} else {
					if (rotation != leftAngle) {
						//TODO: Play lever SFX here
						movingLever = true;
					}
					active = flipLever;
				}
			}
		}
	}
}
