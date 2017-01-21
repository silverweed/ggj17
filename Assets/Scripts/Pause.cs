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

	GameObject pauseButtons;
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
		pauseButtons = transform.FindChild("PauseButtons").gameObject;
		pauseImage = transform.FindChild("PauseImage").gameObject;
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

	public void Resume() {
		if (Paused) { Paused = !Paused; }
	}

	public void SetPaused(bool p, bool showText = true) {
		if (p) {
			StartCoroutine(TransitionToPauseSound());
			wave.enabled = false;
			controls.enabled = false;
			if (showText) {
				pauseButtons.SetActive(true);
				pauseImage.SetActive(true);
			}
		} else {
			StartCoroutine(TransitionToGameSound());
			wave.enabled = true;
			controls.enabled = true;
			pauseButtons.SetActive(false);
			pauseImage.SetActive(false);
		}
	}

	IEnumerator TransitionToPauseSound() {
		var music = Camera.main.GetComponent<AudioSource>();
		var pauseMusic = GetComponent<AudioSource>();
		pauseMusic.volume = 0f;
		pauseMusic.Play();
		float volume = 0f;
		while (Paused && volume < 1f) {
			volume += Time.deltaTime * 5f;
			music.volume = 1f - volume;
			pauseMusic.volume = volume;
			yield return null;
		}
		if (Paused) {
			pauseMusic.volume = 1f;
			music.Pause();
		}
	}

	IEnumerator TransitionToGameSound() {
		var music = Camera.main.GetComponent<AudioSource>();
		var pauseMusic = GetComponent<AudioSource>();
		music.volume = 0f;
		music.UnPause();
		float volume = 0f;
		while (!Paused && volume < 1f) {
			volume += Time.deltaTime * 5f;
			music.volume = volume;
			pauseMusic.volume = 1f - volume;
			yield return null;
		}
		if (!Paused) {
			music.volume = 1f;
			pauseMusic.Stop();
		}
	}
}
