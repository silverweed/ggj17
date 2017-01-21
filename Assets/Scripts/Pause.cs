﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public static Pause Instance {
		get;
		private set;
	}

	GameObject pauseButtons;
	GameObject pauseImage;
	Wave wave;
	Controls controls;
	bool paused;
	ShowPad pad;

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
		pad = GameObject.FindObjectOfType<ShowPad>();
		SetPaused(false);
	}
	
	void Update() {
		// Pause game
		if (Input.GetKeyDown(KeyCode.JoystickButton7) && !pad.Active) { // start
			SetPaused(!paused);
			return;
		}
	}

	public void Resume() {
		if (paused) { SetPaused(false); }
	}

	public void SetPaused(bool p, bool showText = true) {
		paused = p;
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
		while (paused && volume < 1f) {
			volume += Time.deltaTime * 5f;
			music.volume = 1f - volume;
			pauseMusic.volume = volume;
			yield return null;
		}
		if (paused) {
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
		while (!paused && volume < 1f) {
			volume += Time.deltaTime * 5f;
			music.volume = volume;
			pauseMusic.volume = 1f - volume;
			yield return null;
		}
		if (!paused) {
			music.volume = 1f;
			pauseMusic.Stop();
		}
	}
}
