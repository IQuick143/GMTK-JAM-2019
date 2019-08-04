using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalForceField : MonoBehaviour {

	public float ySpeed;
	public float Force = 1f;
	
	private void OnTriggerStay2D(Collider2D collision) {
		Rigidbody2D colRb = collision.attachedRigidbody;
		if (colRb != null) {
			//TODO: Add an acceleration so it feels better
			if (colRb.velocity.y < ySpeed) colRb.AddForce(-Physics2D.gravity + Force * Vector2.up);
			//colRb.velocity = new Vector3(colRb.velocity.x, ySpeed);
		}
	}
}
