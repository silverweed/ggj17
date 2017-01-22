using UnityEngine;
using UnityEngine.UI;

public class WaveText : MonoBehaviour {

	static float sphase = 0f;

	float frequency = 2f;
	float dphase = 0.15f;

	float phase;
	int minSize = 15;
	int maxSize = 38;
	Text text;

	void Start() {
		text = GetComponent<Text>();
		phase = sphase;
		sphase += dphase;
	}

	void Update() {
		text.fontSize = (int)((maxSize - minSize) *
			Mathf.Abs(Mathf.Sin(frequency * Time.time + phase)) + minSize);
	}
}
