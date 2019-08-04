using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IndestructibleSetup))]
public class Settings : MonoBehaviour {
	public static Settings settings;
	void Awake() {
		if (settings == null) settings = this;
		if (settings != this) return;
	}
}
