using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO:
/*
INGAME MENU with PAUSE BUTTON
PAUSE MENU
SETTINGS MENU?
CREDITS???
FADE OUT/FADE IN
STORY
*/

[RequireComponent(typeof(IndestructibleSetup))]
[RequireComponent(typeof(Canvas))]
public class Menu : MonoBehaviour {
	[SerializeField]
	private Canvas ingameMenu;
	[SerializeField]
	private Canvas pauseMenu;
	[SerializeField]
	private CanvasGroup levelFade;
	[SerializeField]
	private Canvas transitionMenu;
	[SerializeField]
	private Text transitionMenuStoryText;

	[SerializeField]
	private float fadeTime = 1f;
	private float fadeTick = 0.03f;
	private bool skipped = false;
	private Menu menu;

	// Start is called before the first frame update
	void Start() {
		if (menu == null) menu = this;
		if (menu != this) return;
		this.levelFade.gameObject.SetActive(false);
		this.ingameMenu.gameObject.SetActive(true);
		this.pauseMenu.gameObject.SetActive(false);

		this.transitionMenu.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		
	}

	public IEnumerator Fade() {
		this.levelFade.gameObject.SetActive(true);
		for (float timer = fadeTime; timer >= 0f; timer -= fadeTick) {
			this.levelFade.alpha = 1f - Mathf.Clamp01(timer / fadeTime);
			yield return new WaitForSecondsRealtime(fadeTick);
		}
		this.levelFade.alpha = 1f;
	}

	public IEnumerator UnFade() {
		for (float timer = fadeTime; timer > 0f; timer -= fadeTick) {
			this.levelFade.alpha = Mathf.Clamp01(timer / fadeTime);
			yield return new WaitForSecondsRealtime(fadeTick);
		}
		this.levelFade.alpha = 0f;
		this.levelFade.gameObject.SetActive(false);
		this.transitionMenu.gameObject.SetActive(false);
	}

	public IEnumerator LevelLoaded(int levelID) {
		Pause.pause.UnPauseGame();
		yield return StartCoroutine(UnFade());
	}

	public void GamePaused() {
		this.ingameMenu.gameObject.SetActive(false);
		this.pauseMenu.gameObject.SetActive(true);
	}

	public void GameUnPaused() {
		this.ingameMenu.gameObject.SetActive(true);
		this.pauseMenu.gameObject.SetActive(false);
	}

	public IEnumerator InStory(int levelID) {
		this.transitionMenu.gameObject.SetActive(true);
		this.transitionMenu.gameObject.SetActive(true);
		this.transitionMenuStoryText.text = "PLACEHOLDER";
		yield return StartCoroutine(Fade());
	}

	public IEnumerator WaitForSkip() {
		this.skipped = false;
		while (!this.skipped) {
			yield return new WaitForSecondsRealtime(fadeTick);
		}
	}

	public void SkipPressed() {
		this.skipped = true;
	}
}
