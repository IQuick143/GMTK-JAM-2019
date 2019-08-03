using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	//Movement data
	public float MaxSpeed = 2.75f;
	public float Acceleration = 5.75f;
	public float Deceleration = 7.5f;

	//Mouse control data
	//Min distance to start counting as movement
	public float MinMouseDist = 0.1f;
	//When to consider the mouse to be so far, we need to reach max speed
	public float MaxMouseDist = 1f;
	public float DistanceAccel = 0;

	//Assignable variables
	[SerializeField]
	private Camera mainCamera;

    //Variables
    private float speed = 0f;
    private float targetSpeed = 0f;
    private Rigidbody2D rb;

	//Booleans probably used mainly for animation
	private bool walking = false;
	private bool turning = false;
	private bool braking = false;
	private bool facingRight = true;

	void Start() {
        DistanceAccel = MaxSpeed / (MaxMouseDist - MinMouseDist);
        rb = GetComponent<Rigidbody2D>();
    }

	void Update() {
		//TODO: Change this line when we all agree on the constants
		DistanceAccel = MaxSpeed / (MaxMouseDist - MinMouseDist);
		/*/Figure out a correct target speed
		if (GetMouseDown()) {
			float mouseDeltaX = GetMousePosition();
			float absMouseDeltaX = Mathf.Abs(mouseDeltaX);
			float sign = Mathf.Sign(mouseDeltaX);
			if (absMouseDeltaX < MinMouseDist) {
				this.targetSpeed = 0;
			} else {
				if (absMouseDeltaX < MaxMouseDist) {
					this.targetSpeed = (absMouseDeltaX - MinMouseDist) * DistanceAccel;
				} else {
					this.targetSpeed = MaxSpeed;
				}
				this.targetSpeed = Mathf.Clamp(this.targetSpeed, 0, MaxSpeed) * sign;
			}
		} else {
			this.targetSpeed = 0;
		}
		//*/
	}

	void FixedUpdate() {
        this.speed = rb.velocity.x;

		if (this.targetSpeed != this.speed) {
			float accel = 0f;

			int signReal = (int) Mathf.Sign(this.speed);
			if (this.speed == 0) signReal *= 0;
			int signTarget = (int) Mathf.Sign(this.targetSpeed);
			if (this.targetSpeed == 0) signTarget *= 0;

			this.braking = !(signReal == 0 || signReal == signTarget);
			if (braking) {
				accel = Deceleration;
			} else {
				accel = Acceleration;
			}
			float deltaV = accel * Time.fixedDeltaTime;
			if (Mathf.Abs(this.targetSpeed - speed) < deltaV) {
                speed = this.targetSpeed;
			} else {
				int accelDirection = (int) Mathf.Sign(this.targetSpeed - speed);
                speed += accelDirection * deltaV;
			}

			if (this.speed < 0) this.facingRight = false;
			if (this.speed > 0) this.facingRight = true;
			
			
		} else {
			this.braking = false;
		}

		this.walking = Mathf.Abs(this.speed) > 0.05f;
		this.turning = this.walking && Mathf.Sign(this.speed) != Mathf.Sign(this.targetSpeed) && Mathf.Sign(this.targetSpeed) != 0;

        rb.velocity = new Vector2(speed, rb.velocity.y);
	}

	//These two methods are for easy portability and input bug fixing
	//The x value difference between player and mouse
	float GetMousePosition() {
		return this.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -this.mainCamera.transform.position.z)).x - this.transform.position.x;
	}

	bool GetMouseDown() {
		return Input.GetMouseButton(0);
	}
}
