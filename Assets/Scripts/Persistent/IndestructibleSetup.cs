using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Menu))]
[RequireComponent(typeof(Pause))]
[RequireComponent(typeof(Settings))]
[RequireComponent(typeof(SceneSwitcher))]
public class IndestructibleSetup : MonoBehaviour {
	[SerializeField]
	private LevelDataScriptableObject levelData;
	// Start is called before the first frame update
	void Awake() {
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
