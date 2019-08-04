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

[RequireComponent(typeof(Canvas))]
public class Menu : MonoBehaviour {
	[SerializeField]
	private Canvas ingameMenu;
	[SerializeField]
	private Canvas pauseMenu;
	[SerializeField]
	private Canvas levelFade;
	[SerializeField]
	private Image levelFadeImage;
	[SerializeField]
	private Canvas transitionMenu;
	[SerializeField]
	private Text transitionMenuStoryText;

	// Start is called before the first frame update
	void Start() {
		this.levelFade.gameObject.SetActive(false);
		this.ingameMenu.gameObject.SetActive(true);
		this.pauseMenu.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		
	}

	public void Fade() {
		this.levelFade.gameObject.SetActive(true);
	}

	public void UnFade() {
		this.levelFade.gameObject.SetActive(false);
	}

	public void LevelLoaded(int levelID) {
		UnFade();
	}

	public void GamePaused() {
		this.ingameMenu.gameObject.SetActive(false);
		this.pauseMenu.gameObject.SetActive(true);
	}

	public void GameUnPaused() {
		this.ingameMenu.gameObject.SetActive(true);
		this.pauseMenu.gameObject.SetActive(false);
	}

	public void FadeInWithStory(int levelID) {
		Fade();
	}
}
