using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Actuator {
	[SerializeField]
	private ElevatorMode mode = ElevatorMode.Repeating;
	[SerializeField]
	private float EndWaitTime = 0.5f;

	[SerializeField]
	private Transform PointA = null;
	[SerializeField]
	private Transform PointB = null;
	[SerializeField]
    private float platformSpeed = 3f;

    private bool isActive;
    private float progress = 0f;
	private float distance;
	private int direction = 0;
	private float waiting = 0f;

	void Awake() {
		this.transform.position = this.PointA.position;
	}

    private void FixedUpdate() {
		this.transform.position = (1f - progress) * this.PointA.position + progress * this.PointB.position;
        if (this.isActive) {
			if (this.waiting > 0f) {
				this.waiting -= Time.fixedDeltaTime;
			} else {
				distance = Mathf.Max(0.1f, (this.PointA.position - this.PointB.position).magnitude);
				progress += direction * platformSpeed * Time.fixedDeltaTime / distance;
				if (progress <= 0) {
					progress = 0f;
					this.direction = 1;
				}
				if (progress >= 1) {
					progress = 1f;
					this.direction = -1;
				}
				if (progress == 1f || progress == 0f) {
					if (this.mode == ElevatorMode.OneWay) {
						this.isActive = false;
					} else {
						this.waiting = EndWaitTime;
					}
				}
			}
        }
    }

    public override void OnActivate() {
		if (this.mode == ElevatorMode.OneWay) {
			this.direction = 1;
			this.isActive = true;
		} else {
			this.isActive = true;
		}
    }

    public override void OnDeactivate() {
		if (this.mode == ElevatorMode.OneWay) {
			this.direction = -1;
			this.isActive = true;
		} else {
			this.isActive = false;
		}
    }
}

public enum ElevatorMode {
	Repeating,
	OneWay
}
