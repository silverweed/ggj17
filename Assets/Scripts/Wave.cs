using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	int POINTS = Mathf.Min(Screen.width, 1080);

	public Wave.Shape ShapeForCode{
		get {
			return shape;
		}
		set {
			shape = value;
			GameObject.FindObjectOfType<Electron>().ChangedWaveShape(value);
		}
	}

	public bool overrideColor;
	public Color color;
	public Wave.Shape shape;
	public Material waveMaterial;
	public float waveThickness = 0.1f;
	public float amplitude;
	public float frequency;
	public float speed;
	public float phase;
	public float offset;

	new LineRenderer renderer;
	Vector3[] points;
	float step;

	Transform particle;

	void Awake() {
		POINTS = Mathf.Min(1080, Screen.width /2);
		points = new Vector3[POINTS];
		renderer = gameObject.AddComponent<LineRenderer>();
		renderer.numPositions = POINTS;
		renderer.startWidth = waveThickness;
		renderer.material = waveMaterial;
		step = ScreenWidth() / (renderer.numPositions - 1);
		particle = GameObject.Find("Particle").transform;
	}

	void Start() {
		if (overrideColor)
			DynamicGI.SetEmissive(renderer.GetComponent<Renderer>(), color);
	ShapeForCode = shape;
	}

	float ScreenWidth() {
		var left = Camera.main.ScreenToWorldPoint(Vector3.zero);
		var right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f));
		return right.x - left.x;
	}

	void Update() {
		float offsetDiv = 8f;
		var offset = ScreenWidth() / offsetDiv;
		for (int i = 0; i < points.Length; ++i) {
			renderer.SetPosition(i, new Vector3(this.offset + step * i - offset,
				WaveAt(ShapeForCode, step * i - offset), 1f));
		}
		this.offset += Time.deltaTime * speed;
		phase += Time.deltaTime * frequency;
		particle.position = new Vector3(this.offset, WaveAt(ShapeForCode, 0), 1f);
		Camera.main.transform.position = new Vector3(this.offset + offset *(offsetDiv/2f -1f),
			Camera.main.transform.position.y, Camera.main.transform.position.z);
	}

	float WaveAt(Wave.Shape shape, float offsetInScreen) {
		switch (shape) {
			case Wave.Shape.SINE: return Sine(offsetInScreen);
			case Wave.Shape.SAW: return Saw(offsetInScreen);
			case Wave.Shape.TRIANGLE: return Triangle(offsetInScreen);
			case Wave.Shape.SQUARE: return Square(offsetInScreen);
			default: return 0;
		}
	}

	float Sine(float offsetInScreen) {
		return amplitude * Mathf.Sin(frequency * offsetInScreen + speed * phase);
	}

	float Saw(float offsetInScreen) {
		var tan = Mathf.Tan((frequency * offsetInScreen / 5f + speed / 5f * phase) * Mathf.PI);
		return 2 * amplitude / Mathf.PI * Mathf.Atan(tan);
	}

	float Triangle(float offsetInScreen) {
		var sin = Mathf.Sin(2 * Mathf.PI * frequency / (2 * Mathf.PI) * offsetInScreen + speed * phase);
		return 2 * amplitude / Mathf.PI * Mathf.Asin(sin);
	}

	float Square(float offsetInScreen) {
		return amplitude * Mathf.Sign(Mathf.Sin(frequency * offsetInScreen + speed * phase));
	}

	public enum Shape {
		SINE,
		SAW,
		TRIANGLE,
		SQUARE
	}
}
