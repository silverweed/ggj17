using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

	public bool willAmplitudeBeActive;
	public bool willFrequencyBeActive;
	public bool willShapeBeActive;
	public bool willSpeedBeActive;

	void OnTriggerEnter2D(Collider2D cld) {
		print("Activate trigger " + this);
		var controls = GameObject.FindObjectOfType<Controls>();
		controls.canChangeAmplitude = willAmplitudeBeActive;
		controls.canChangeFrequency = willFrequencyBeActive;
		controls.canChangeForm = willShapeBeActive;
		controls.canChangeSpeed = willSpeedBeActive;
	}
}
