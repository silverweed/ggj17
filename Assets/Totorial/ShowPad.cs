using UnityEngine.UI;
using UnityEngine;

public class ShowPad : MonoBehaviour {
	
	public enum ShowType {
		LEFT_H,
		LEFT_V,
		SINE,
		TRIANGLE,
		SQUARE,
		SAW
	}

	public ShowType showed;

	TotorialPad pad;

	void Start() {
		pad = GameObject.FindObjectOfType<TotorialPad>();
	}

	void Update() {
		if (PressedRightButton()) {
			Pause.Instance.SetPaused(false);
			pad.Hide();
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		switch (showed) {
		case ShowType.LEFT_H:
			pad.ShowLeftHorizontal();
			break;
		case ShowType.LEFT_V:
			pad.ShowLeftVertical();
			break;
		case ShowType.SINE:
			pad.ShowSin();
			break;
		case ShowType.TRIANGLE:
			pad.ShowTriangle();
			break;
		case ShowType.SAW:
			pad.ShowSawtooth();
			break;
		case ShowType.SQUARE:
			pad.ShowSquare();
			break;
		}

		Pause.Instance.SetPaused(true, false);
	}

	bool PressedRightButton() {
		switch (showed) {
		case ShowType.LEFT_H:
			return Input.GetAxis("Horizontal") != 0;
		case ShowType.LEFT_V:
			return Input.GetAxis("Vertical") != 0;
		case ShowType.SINE:
			return Input.GetKey(Controls.mapping[Wave.Shape.SINE]);
		case ShowType.TRIANGLE:
			return Input.GetKey(Controls.mapping[Wave.Shape.TRIANGLE]);
		case ShowType.SAW:
			return Input.GetKey(Controls.mapping[Wave.Shape.SAW]);
		case ShowType.SQUARE:
			return Input.GetKey(Controls.mapping[Wave.Shape.SQUARE]);
		}
		return false;
	}
}
