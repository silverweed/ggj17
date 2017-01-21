﻿using System.Collections;
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
            StartCoroutine(TransitionToPauseSound());
			wave.enabled = false;
			controls.enabled = false;
			pauseText.SetActive(true);
			pauseImage.SetActive(true);
		} else {
            StartCoroutine(TransitionToGameSound());
            wave.enabled = true;
			controls.enabled = true;
			pauseText.SetActive(false);
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
