using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{

	public bool willAmplitudeBeActive;
	public bool willFrequencyBeActive;
	public bool willShapeBeActive;
	public bool willSpeedBeActive;
	public bool willSquareBeActive;
	public bool willTriangleBeActive;
	public bool willSawBeActive;
	public bool willSineBeActive;

	void OnTriggerEnter2D (Collider2D cld){
		print("Activate trigger " + this);
		var controls = GameObject.FindObjectOfType<Controls>();
		controls.canChangeAmplitude = willAmplitudeBeActive;
		controls.canChangeFrequency = willFrequencyBeActive;
		controls.canChangeForm = willShapeBeActive;
		controls.canChangeSpeed = willSpeedBeActive;
		if (willSquareBeActive) {
			controls.allowedShapes.Add(Wave.Shape.SQUARE);
		} else {
			controls.allowedShapes.Remove(Wave.Shape.SQUARE);
		}
		if (willTriangleBeActive) {
			controls.allowedShapes.Add(Wave.Shape.TRIANGLE);
		} else {
			controls.allowedShapes.Remove(Wave.Shape.TRIANGLE);
		}
		if (willSawBeActive) {
			controls.allowedShapes.Add(Wave.Shape.SAW);
		} else {
			controls.allowedShapes.Remove(Wave.Shape.SAW);
		}
		if (willSineBeActive) {
			controls.allowedShapes.Add(Wave.Shape.SINE);
		} else {
			controls.allowedShapes.Remove(Wave.Shape.SINE);
		}
	}
}
