using UnityEngine;
using System;

/// <summary>
/// Provides events for single-fire actions on joystick moved
/// (both up/down and left/right)
/// </summary>
public class JoystickListener : MonoBehaviour {

	public event Action OnJoystickLeft;
	public event Action OnJoystickRight;
	public event Action OnJoystickDown;
	public event Action OnJoystickUp;

	public float threshold = 0.6f;

	bool isHZero = true,
	     isVZero = true;

	void Update() {
		float hAxis = Input.GetAxis("Horizontal"),
		      vAxis = Input.GetAxis("Vertical");

		if (isHZero) {
			if (hAxis < -threshold) {
				isHZero = false;
				if (OnJoystickLeft != null)
					OnJoystickLeft();
			} else if (hAxis > threshold) {
				isHZero = false;
				if (OnJoystickRight != null)
					OnJoystickRight();
			}
		} else if (Mathf.Approximately(hAxis, 0)) {
			isHZero = true;
		}
		if (isVZero) {
			if (vAxis < -threshold) {
				isVZero = false;
				if (OnJoystickUp != null)
					OnJoystickUp();
			} else if (vAxis > threshold) {
				isVZero = false;
				if (OnJoystickDown != null)
					OnJoystickDown();
			}
		} else if (Mathf.Approximately(vAxis, 0)) {
			isVZero = true;
		}
	}
}
