using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	const float AMPLITUDE_CHANGE_SPEED = 30f;
	const float FREQUENCY_CHANGE_SPEED = 0.02f;
	const float SPEED_CHANGE_SPEED = 80f;
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
		      hAxis = Input.GetAxis("Horizontal"),
		      rhAxis = Input.GetAxis("RightH");
		if (vAxis > 0.3f)
			wave.amplitude += AMPLITUDE_CHANGE_SPEED * Time.deltaTime;
		else if (vAxis < -0.3f)
			wave.amplitude -= AMPLITUDE_CHANGE_SPEED * Time.deltaTime;
		if (hAxis > 0.4f)
			wave.frequency += FREQUENCY_CHANGE_SPEED * Time.deltaTime;
		else if (hAxis < -0.4f)
			wave.frequency -= FREQUENCY_CHANGE_SPEED * Time.deltaTime;
		if (rhAxis > 0.3f)
			wave.speed += SPEED_CHANGE_SPEED * Time.deltaTime;
		else if (rhAxis < -0.3f)
			wave.speed -= SPEED_CHANGE_SPEED * Time.deltaTime;

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
