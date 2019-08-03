﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	//Movement data
	const float MaxSpeed = 2f;
	const float Acceleration = 5f;
	const float Deceleration = 6f;

	//Mouse control data
	//Min distance to start counting as movement
	const float MinMouseDist = 0.1f;
	//When to consider the mouse to be so far, we need to reach max speed
	const float MaxMouseDist = 1f;
	const float DistanceAccel = MaxSpeed / (MaxMouseDist - MinMouseDist);

	//Assignable variables
	[SerializeField]
	private Camera mainCamera;

	//variables
	[SerializeField]
	private float speed = 0f;
	[SerializeField]
	private float targetSpeed = 0f;

	void Start() {
		
	}

	void Update() {
		//Figure out a correct target speed
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
	}

	void FixedUpdate() {
		if (this.targetSpeed != this.speed) {
			float accel = 0f;
			int signReal = (int) Mathf.Sign(this.speed);
			if (this.speed == 0) signReal *= 0;
			int signTarget = (int) Mathf.Sign(this.targetSpeed);
			if (this.targetSpeed == 0) signTarget *= 0;
			if (signReal == 0 || signReal == signTarget) {
				accel = Acceleration;
			} else {
				accel = Deceleration;
			}
			float deltaV = accel * Time.fixedDeltaTime;
			if (Mathf.Abs(this.targetSpeed - this.speed) < deltaV) {
				this.speed = this.targetSpeed;
			} else {
				int accelDirection = (int) Mathf.Sign(this.targetSpeed - this.speed);
				this.speed += accelDirection * deltaV;
			}
		}
		this.transform.position += Vector3.right * this.speed * Time.fixedDeltaTime;
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
