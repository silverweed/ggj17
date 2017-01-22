using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
	
	public GameObject credits;

	int selected = -1;
	Button[] buttons;
	JoystickListener joyLst;

	void Start() {
		buttons = new Button[3];
		buttons[0] = GameObject.Find("PlayBtn").GetComponent<Button>();
		buttons[1] = GameObject.Find("CreditsBtn").GetComponent<Button>();
		buttons[2] = GameObject.Find("QuitBtn").GetComponent<Button>();
		joyLst = gameObject.AddComponent<JoystickListener>();
		joyLst.OnJoystickUp += SelectNext(true);
		joyLst.OnJoystickDown += SelectNext(false);
        MenuMusic.StartMusic();
		SelectNext(false)();
	}

	public void Play() {
		SceneManager.LoadScene("Selection");
	}

	public void Credits() {
		credits.SetActive(true);
		credits.GetComponentInChildren<Button>().Select();
	}

	public void CloseCredits() {
		credits.SetActive(false);
	}

	public void Quit() {
		Application.Quit();
	}

	System.Action SelectNext(bool up) {
		return () => {
			if (selected < 0)
				selected = 0;
			else {
				selected = (selected + (up ? -1 : 1)) % buttons.Length;
				if (selected < 0) selected += buttons.Length;
			}

			buttons[selected].Select();
		};
	}
}
