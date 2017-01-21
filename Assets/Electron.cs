﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Electron : MonoBehaviour {

	public int life = 1;

	Wave wave;

	void Start() {
		wave = GameObject.FindObjectOfType<Wave>();
	}
	
	void Update() {
		// TODO: follow wave
		transform.position = wave.ElecronPosition;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// TODO: game over
		if (--life == 0) {
			Debug.Log("You lose");
			SceneManager.LoadScene("Main");
		}
	}
}
