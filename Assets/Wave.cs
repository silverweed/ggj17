using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveForm {
	SINE,
	SAW,
	TRIANGLE,
	SQUARE
}

public class Wave : MonoBehaviour {

	const int POINTS_NUM = 200;

	public WaveForm waveForm;
	public float amplitude;
	[Range(0.01f, 0.1f)]
	public float frequency;
	public float speed = 20;

	LineRenderer renderer;
	Vector3[] points;
	float step;

	public Vector3 ElecronPosition {
		get {
			return (renderer.GetPosition(9) + renderer.GetPosition(10)) / 2f;
		}
	}

	void Awake() {
		points = new Vector3[POINTS_NUM];
		renderer = gameObject.AddComponent<LineRenderer>();
		renderer.numPositions = POINTS_NUM;
		renderer.startWidth = 0.1f;
		waveForm = WaveForm.SINE;
		amplitude = 10f;
		frequency = 1f;

		// Get the screen width to obtain the step for
		// calculating wave values in the LineRenderer points.
		step = (float)Screen.width / (renderer.numPositions - 1);
	}
	
	void Update() {
		for (int i = 0; i < points.Length; ++i) {
			renderer.SetPosition(i, Camera.main.ScreenToWorldPoint(new Vector3(
						step * i,
						Screen.height/2f + WaveAt(waveForm, step * i),
						1)));
		}
	}

	// Returns the value of the waveform w in position x
	float WaveAt(WaveForm w, float x) {
		switch (w) {
		case WaveForm.SINE:
			return Sine(x);
		case WaveForm.SAW:
			return Saw(x);
		case WaveForm.TRIANGLE:
			return Triangle(x);
		case WaveForm.SQUARE:
			return Square(x);
		}
		return 0;
	}

#region WaveCalculations
	float Sine(float x) {
		return amplitude * Mathf.Sin(frequency * x + speed * Time.time);
	}

	float Saw(float x) {
		return -2 * amplitude / Mathf.PI * Mathf.Atan(1f / Mathf.Tan(
					(frequency/10f * x + speed/4f * Time.time) * Mathf.PI));
	}

	float Triangle(float x) {
		return 2 * amplitude / Mathf.PI * Mathf.Asin(Mathf.Sin(
					2 * Mathf.PI * frequency/10f * x + speed/2f * Time.time));
	}

	float Square(float x) {
		return amplitude * Mathf.Sign(Mathf.Sin(frequency * x + speed * Time.time));
	}
#endregion
}
