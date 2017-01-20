using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour {

	Wave wave;

	void Start() {
		wave = GameObject.FindObjectOfType<Wave>();
	}
	
	void Update() {
		// TODO: follow wave
		transform.position = wave.ElecronPosition;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// TODO: game over
	}
}
