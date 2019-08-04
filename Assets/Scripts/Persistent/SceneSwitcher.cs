using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(IndestructibleSetup))]
public class SceneSwitcher : MonoBehaviour {
	public static SceneSwitcher switcher;
	private int currentSceneID = 0;
	public Menu menu;

	// Start is called before the first frame update
	void Start() {
		if (switcher == null) switcher = this;
		if (switcher != this) return;
		menu.LevelLoaded(currentSceneID);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
    }

    public static void OnLevelCompleted() {
		SceneSwitcher.switcher.StartCoroutine(SceneSwitcher.switcher.LevelCompleted());
	}

	public IEnumerator LevelCompleted() {
		//Fade out + story
		yield return StartCoroutine(menu.InStory(currentSceneID));
		currentSceneID = (currentSceneID+1) % SceneManager.sceneCountInBuildSettings;
		SceneManager.LoadScene(currentSceneID);
		yield return StartCoroutine(menu.WaitForSkip());
		//Fade in
		yield return StartCoroutine(menu.LevelLoaded(currentSceneID));
	}

	public void ResetLevel() {
		StartCoroutine(SceneSwitcher.switcher.ResetLevelRoutine());
	}

	public IEnumerator ResetLevelRoutine() {
		//Fade out
		yield return StartCoroutine(menu.Fade());
		SceneManager.LoadScene(currentSceneID);
		//Fade in
		yield return StartCoroutine(menu.LevelLoaded(currentSceneID));
	}
}
