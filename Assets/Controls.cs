using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	const float AMPLITUDE_CHANGE_SPEED = 40f;
	const float FREQUENCY_CHANGE_SPEED = 1f;
	const float SPEED_CHANGE_SPEED = 80f;

	public float WaveMaxFreq=2f;
	public float WaveMinFreq=0.35f;
	public float WaveMaxAmp=3.5f;
	public float WaveMinAmp=0.1f;

	public bool canChangeAmplitude = true,
	            canChangeFrequency = true,
	            canChangeForm = true,
	            canChangeSpeed;

	WavePls wave;

	void Start() {
		wave = GameObject.FindObjectOfType<WavePls>();
#if DEBUG
		canChangeSpeed = true;
#endif
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
		if (canChangeAmplitude) {
			if (vAxis > 0.3f)
				wave.amplitude += AMPLITUDE_CHANGE_SPEED * Time.deltaTime;
			else if (vAxis < -0.3f)
				wave.amplitude -= AMPLITUDE_CHANGE_SPEED * Time.deltaTime;
		}
		if (canChangeFrequency) {
			if (hAxis > 0.4f)
				wave.frequency -= FREQUENCY_CHANGE_SPEED* (1f - Mathf.Pow(wave.frequency/(WaveMaxFreq-WaveMinFreq),1.2f))
				   	* Time.deltaTime;
			else if (hAxis < -0.4f)
				wave.frequency += FREQUENCY_CHANGE_SPEED * (1f - Mathf.Pow(wave.frequency/(WaveMaxFreq-WaveMinFreq),1.2f))
					* Time.deltaTime;
		}
		if (canChangeSpeed) {
			if (rhAxis > 0.7f)
				wave.speed += SPEED_CHANGE_SPEED * Time.deltaTime;
			else if (rhAxis < -0.7f)
				wave.speed -= SPEED_CHANGE_SPEED * Time.deltaTime;
		}

		wave.frequency = Mathf.Clamp(wave.frequency, WaveMinFreq,WaveMaxFreq);
		wave.amplitude = Mathf.Clamp(wave.amplitude, WaveMinAmp, WaveMaxAmp );

		if (canChangeForm) {
			if (Input.GetKey(KeyCode.JoystickButton0)) // A
				wave.waveForm = WavePls.Shape.SINE;
			else if (Input.GetKey(KeyCode.JoystickButton1)) // B
				wave.waveForm = WavePls.Shape.TRIANGLE;
			else if (Input.GetKey(KeyCode.JoystickButton2)) // X
				wave.waveForm = WavePls.Shape.SAW;
			else if (Input.GetKey(KeyCode.JoystickButton3)) // Y
				wave.waveForm = WavePls.Shape.SQUARE;
		}
	}
}
