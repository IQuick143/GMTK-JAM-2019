using UnityEngine;

public class Objective : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			SceneSwitcher.OnLevelCompleted();
		}
	}
}
