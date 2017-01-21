using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public static Pause Instance {
		get;
		private set;
	}

	public bool Paused {
		get { return paused; }
		set {
			paused = value;
			SetPaused(paused);
		}
	}

	GameObject pauseText;
	GameObject pauseImage;
	Wave wave;
	Controls controls;
	bool paused;

	private Pause() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	void Start() {
		pauseText = GameObject.Find("PauseText");
		pauseImage = GameObject.Find("PauseImage");
		wave = GameObject.FindObjectOfType<Wave>();
		controls = GameObject.FindObjectOfType<Controls>();
		SetPaused(false);
	}
	
	void Update() {
		// Pause game
		if (Input.GetKeyDown(KeyCode.JoystickButton7)) { // start
			Paused = !Paused;
			return;
		}
	}

	void SetPaused(bool p) {
		if (p) {
			wave.enabled = false;
			controls.enabled = false;
			pauseText.SetActive(true);
			pauseImage.SetActive(true);
		} else {
			wave.enabled = true;
			controls.enabled = true;
			pauseText.SetActive(false);
			pauseImage.SetActive(false);
		}
	}
}
