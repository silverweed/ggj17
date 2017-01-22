using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsScroller : MonoBehaviour {

	static readonly string[] LEVELS = {
		"Tutorial0",
		"Tutorial1_graficaElisa",
		"Levellolo3_graficaElisa",
		"Elegance_graficaElisa",
		//"Levellolo4",
    };

	static string lastPlayedLevel;

	List<RectTransform> levels = new List<RectTransform>();
	int showedLevel = 0;
	Button back;
	bool theySeeMeScrollinTheyHatin = false;
	bool onButton = false;

	void Start() {
		foreach (Transform child in transform) { if (child.gameObject.activeSelf) { levels.Add(child.GetComponent<RectTransform>()); } }
		var joystick = GameObject.FindObjectOfType<JoystickListener>();
		back = GameObject.Find("Back").GetComponent<Button>();
		joystick.OnJoystickLeft += () => StartCoroutine(ScrollRight());
		joystick.OnJoystickRight += () => StartCoroutine(ScrollLeft());
		joystick.OnJoystickDown += SelectBackButton;
		joystick.OnJoystickUp += DeselectBackButton;
		if (lastPlayedLevel != null) { SetNextLevel(); }
		levels[showedLevel].gameObject.GetComponent<Button>().Select();
		MenuMusic.StartMusic();
	}

	public static void LastCompletedLevel(string name) {
		lastPlayedLevel = name;
	}

	void SetNextLevel() {
		int index = LEVELS.Length;
		while (index > 0 && lastPlayedLevel != LEVELS[index - 1]) { --index; }
		print(lastPlayedLevel);
		print(index);
		SetShowedLevel(index);
	}

	void SetShowedLevel(int index) {
		index = Mathf.Clamp(index, 0, levels.Count - 1);
		showedLevel = index;
		var offset = levels[showedLevel].anchoredPosition.x;
		foreach (var lvl in levels) {
			lvl.anchoredPosition = new Vector2(lvl.anchoredPosition.x - offset, 0f);
		}
	}

	public void LaunchLevel() {
		gameObject.SetActive(false);
		MenuMusic.StopMusic();
		SceneManager.LoadSceneAsync(LEVELS[showedLevel]);
	}

	public void Back() {
		SceneManager.LoadSceneAsync("Menu");
	}

	IEnumerator ScrollLeft() {
		if (showedLevel == levels.Count - 1) { yield break; }
		if (theySeeMeScrollinTheyHatin || onButton) { yield break; }
		theySeeMeScrollinTheyHatin = true;
		++showedLevel;
		RectTransform level = levels[showedLevel];
		while (level.anchoredPosition.x > 0) {
			foreach (var lvl in levels) {
				lvl.anchoredPosition -= new Vector2(Time.deltaTime * 800, 0f);
			}
			yield return null;
		}
		foreach (var lvl in levels) {
			lvl.anchoredPosition -= new Vector2(level.anchoredPosition.x, 0f);
		}
		levels[showedLevel].gameObject.GetComponent<Button>().Select();
		theySeeMeScrollinTheyHatin = false;
	}

	IEnumerator ScrollRight() {
		if (showedLevel == 0) { yield break; }
		if (theySeeMeScrollinTheyHatin || onButton) { yield break; }
		theySeeMeScrollinTheyHatin = true;
		--showedLevel;
		RectTransform level = levels[showedLevel];
		while (level.anchoredPosition.x < 0) {
			foreach (var lvl in levels) {
				lvl.anchoredPosition += new Vector2(Time.deltaTime * 800, 0f);
			}
			yield return null;
		}
		foreach (var lvl in levels) {
			lvl.anchoredPosition -= new Vector2(level.anchoredPosition.x, 0f);
		}
		levels[showedLevel].gameObject.GetComponent<Button>().Select();
		theySeeMeScrollinTheyHatin = false;
	}

	void SelectBackButton() {
		if (onButton || theySeeMeScrollinTheyHatin) { return; }
		onButton = true;
		back.Select();
	}

	void DeselectBackButton() {
		if (!onButton || theySeeMeScrollinTheyHatin) { return; }
		onButton = false;
		levels[showedLevel].gameObject.GetComponent<Button>().Select();
	}
}
