using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
	public static SceneSwitcher switcher;
	private int currentSceneID = 0;
	public Menu menu;

	// Start is called before the first frame update
	void Start() {
		if (switcher == null) switcher = this;
		if (switcher != this) Destroy(this);
		menu.LevelLoaded(currentSceneID);
	}

	public static void OnLevelCompleted() {
		SceneSwitcher.switcher.LevelCompleted();
	}

	public void LevelCompleted() {
		//Fade out + story
		menu.FadeInWithStory(currentSceneID);
		currentSceneID++;
		SceneManager.LoadScene(currentSceneID);
		//Fade in
		menu.LevelLoaded(currentSceneID);
	}

	public void ResetLevel() {
		//Fade out
		menu.FadeInWithStory(currentSceneID);
		SceneManager.LoadScene(currentSceneID);
		//Fade in
		menu.LevelLoaded(currentSceneID);
	}
}
