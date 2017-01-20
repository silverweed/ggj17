using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	Wave wave;

	void Start() {
		wave = GameObject.FindObjectOfType<Wave>();
	}
	
	void Update() {
		/*
		 * Up/down: change amplitude
		 * left/right: change frequency
		 * buttons: change waveform
		 */
		float vAxis = Input.GetAxis("Vertical"),
		      hAxis = Input.GetAxis("Horizontal");
		if (vAxis > 0.3f)
			wave.amplitude += 20 * Time.deltaTime;
		else if (vAxis < -0.3f)
			wave.amplitude -= 20 * Time.deltaTime;
		if (hAxis > 0.4f)
			wave.frequency += 0.1f * Time.deltaTime;
		else if (hAxis < -0.4f)
			wave.frequency -= 0.1f * Time.deltaTime;

		wave.frequency = Mathf.Clamp(wave.frequency, 0.01f, 0.1f);
		wave.amplitude = Mathf.Clamp(wave.amplitude, 10, 100);

		if (Input.GetKey(KeyCode.JoystickButton0)) // A
			wave.waveForm = WaveForm.SINE;
		else if (Input.GetKey(KeyCode.JoystickButton1)) // B
			wave.waveForm = WaveForm.TRIANGLE;
		else if (Input.GetKey(KeyCode.JoystickButton2)) // X
			wave.waveForm = WaveForm.SAW;
		else if (Input.GetKey(KeyCode.JoystickButton3)) // Y
			wave.waveForm = WaveForm.SQUARE;
	}
}
