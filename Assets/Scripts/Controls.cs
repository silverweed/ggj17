using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

	const float AMPLITUDE_CHANGE_SPEED = 3f;
	const float FREQUENCY_CHANGE_SPEED = 1f;
	const float SPEED_CHANGE_SPEED = 10f;

	public static Dictionary<Wave.Shape, HashSet<KeyCode>> mapping;
	public HashSet<Wave.Shape> allowedShapes;

	public float waveMaxFreq = 2f;
	public float waveMinFreq = 0.35f;
	public float waveMaxAmp = 3.5f;
	public float waveMinAmp = 0.1f;

	public bool canChangeAmplitude = true,
		canChangeFrequency = true,
		canChangeForm = true,
		canChangeSpeed;

	Wave wave;

	static Controls ()
	{
		mapping = new Dictionary<Wave.Shape, HashSet<KeyCode>> () {
			{ Wave.Shape.SINE, new HashSet<KeyCode> (){ KeyCode.JoystickButton0, KeyCode.H } },
			{ Wave.Shape.TRIANGLE, new HashSet<KeyCode> (){ KeyCode.JoystickButton1, KeyCode.J} },
			{ Wave.Shape.SAW, new HashSet<KeyCode> (){ KeyCode.JoystickButton2,KeyCode.K } },
			{ Wave.Shape.SQUARE, new HashSet<KeyCode> (){ KeyCode.JoystickButton3,KeyCode.L } },
		};
	}

	void Start ()
	{
		wave = GameObject.FindObjectOfType<Wave> ();
#if DEBUG
		canChangeSpeed = true;
#endif
	}

	void Update ()
	{
		/*
		 * Up/down: change amplitude
		 * left/right: change frequency
		 * buttons: change waveform
		 */
		float vAxis = Input.GetAxis ("Vertical"),
		hAxis = Input.GetAxis ("Horizontal"),
		rhAxis = Input.GetAxis ("RightH");
		if (canChangeAmplitude) {
			if (vAxis > 0.3f)
				wave.amplitude += AMPLITUDE_CHANGE_SPEED * Time.deltaTime;
			else if (vAxis < -0.3f)
				wave.amplitude -= AMPLITUDE_CHANGE_SPEED * Time.deltaTime;
		}
		if (canChangeFrequency) {
			if (hAxis > 0.4f)
				wave.frequency -= FREQUENCY_CHANGE_SPEED *
				(1f - Mathf.Pow (wave.frequency / (waveMaxFreq - waveMinFreq), 1.2f))
				* Time.deltaTime;
			else if (hAxis < -0.4f)
				wave.frequency += FREQUENCY_CHANGE_SPEED *
				(1f - Mathf.Pow (wave.frequency / (waveMaxFreq - waveMinFreq), 1.2f))
				* Time.deltaTime;
		}
		if (canChangeSpeed) {
			if (rhAxis > 0.7f)
				wave.speed += SPEED_CHANGE_SPEED * Time.deltaTime;
			else if (rhAxis < -0.7f)
				wave.speed -= SPEED_CHANGE_SPEED * Time.deltaTime;
		}

		wave.frequency = Mathf.Clamp (wave.frequency, waveMinFreq, waveMaxFreq);
		wave.amplitude = Mathf.Clamp (wave.amplitude, waveMinAmp, waveMaxAmp);

		if (canChangeForm) {
			foreach (var shape in mapping.Keys) {
				foreach (var key in mapping[shape]) {
					if (Input.GetKey (key)) {
						wave.shape = shape;
					}
				}
			}
		}
	}
}
