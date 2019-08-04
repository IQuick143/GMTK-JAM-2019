using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IndestructibleSetup))]
public class Pause : MonoBehaviour {
	public static Pause pause;
	public bool paused = false;
	public Menu menu;

	void Awake() {
		if (pause == null) pause = this;
		if (pause != this) return;
	}

	public void PauseGame() {
		if (paused) return;
		this.paused = true;
		Time.timeScale = 0f;
		menu.GamePaused();
	}

	public void UnPauseGame() {
		if (!paused) return;
		this.paused = false;
		Time.timeScale = 1f;
		menu.GameUnPaused();
	}
}
