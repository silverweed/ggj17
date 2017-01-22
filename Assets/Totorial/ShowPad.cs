using UnityEngine.UI;
using UnityEngine;

public class ShowPad : MonoBehaviour
{
	
	public enum ShowType
	{
		LEFT_H,
		LEFT_V,
		SINE,
		TRIANGLE,
		SQUARE,
		SAW
	}

	public ShowType showed;

	public bool Active{ get; private set; }

	TotorialPad pad;

	void Start ()
	{
		pad = GameObject.FindObjectOfType<TotorialPad> ();
		Active = false;
		print  ("found this pad: "+pad);
	}

	void Update ()
	{
		if (Active && PressedRightButton ()) {
			Pause.Instance.SetPaused (false);
			Active = false;
			pad.Hide ();
            gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
        if (Active) { return; }
		switch (showed) {
		case ShowType.LEFT_H:
			pad.ShowLeftHorizontal ();
			break;
		case ShowType.LEFT_V:
			pad.ShowLeftVertical ();
			break;
		case ShowType.SINE:
			pad.ShowSin ();
			break;
		case ShowType.TRIANGLE:
			pad.ShowTriangle ();
			break;
		case ShowType.SAW:
			pad.ShowSawtooth ();
			break;
		case ShowType.SQUARE:
			pad.ShowSquare ();
			break;
		}

		Active = true;
		Pause.Instance.SetPaused (true, false);
	}

	bool PressedRightButton ()
	{
		switch (showed) {
		case ShowType.LEFT_H:
			return Input.GetAxis ("Horizontal") != 0;
		case ShowType.LEFT_V:
			return Input.GetAxis ("Vertical") != 0;
		case ShowType.SINE:
			foreach (var key in Controls.mapping[Wave.Shape.SINE]) {
				if (Input.GetKey (key)) {
					return true;
				}
			}
			break;
		case ShowType.TRIANGLE:
			foreach (var key in Controls.mapping[Wave.Shape.TRIANGLE]) {
				if (Input.GetKey (key)) {
					return true;
				}
			}
			break;
		case ShowType.SAW:
			foreach (var key in Controls.mapping[Wave.Shape.SAW]) {
				if (Input.GetKey (key)) {
					return true;
				}
			}
			break;
		case ShowType.SQUARE:
			foreach (var key in Controls.mapping[Wave.Shape.SQUARE]) {
				if (Input.GetKey (key)) {
					return true;
				}
			}
			break;
		}
		return false;
	}
}
