using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour {

	Text text;
	bool descending = true;

	void Start() {
		text = GetComponent<Text>();
		text.color = Color.white;
	}
	
	void Update() {
		var tc = text.color;
		if (descending) {
			Color c = new Color(
					Mathf.Clamp01(tc.r - Time.deltaTime),
					Mathf.Clamp01(tc.g - Time.deltaTime),
					Mathf.Clamp01(tc.b - Time.deltaTime));
			text.color = c;
			if (c.r == 0)
				descending = false;
		} else {
			Color c = new Color(
					Mathf.Clamp01(tc.r + Time.deltaTime),
					Mathf.Clamp01(tc.g + Time.deltaTime),
					Mathf.Clamp01(tc.b + Time.deltaTime));
			text.color = c;
			if (c.r == 1)
				descending = true;
		}
	}
}
