using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Menu))]
[RequireComponent(typeof(Pause))]
[RequireComponent(typeof(Settings))]
[RequireComponent(typeof(SceneSwitcher))]
public class IndestructibleSetup : MonoBehaviour {
	private static IndestructibleSetup setup;
	[SerializeField]
	private LevelDataScriptableObject levelData;
	// Start is called before the first frame update
	void Awake() {
		if (setup == null) setup = this;
		if (setup != this) Destroy(this.gameObject);
		Object.DontDestroyOnLoad(this.gameObject);
		Menu menu = this.GetComponent<Menu>();
		Pause pause = this.GetComponent<Pause>();
		SceneSwitcher sceneSwitcher = this.GetComponent<SceneSwitcher>();
		pause.menu = menu;
		sceneSwitcher.menu = menu;
	}

	// Update is called once per frame
	void Update() {
		
	}
}
