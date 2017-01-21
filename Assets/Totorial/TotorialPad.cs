using UnityEngine;

public class TotorialPad : MonoBehaviour {

    GameObject pad;
    GameObject leftHorizontal;
    GameObject leftVertical;
    GameObject sin;
    GameObject square;
    GameObject sawtooth;
    GameObject triangle;

    void Awake() {
        pad = transform.FindChild("Pad").gameObject;
        leftHorizontal = transform.FindChild("LeftHorizontal").gameObject;
        leftVertical = transform.FindChild("LeftVertical").gameObject;
        sin = transform.FindChild("Sin").gameObject;
        square = transform.FindChild("Square").gameObject;
        sawtooth = transform.FindChild("Sawtooth").gameObject;
        triangle = transform.FindChild("Triangle").gameObject;
        pad.SetActive(false);
        foreach (Transform child in transform) { child.gameObject.SetActive(false); }
    }

    public void ShowLeftHorizontal() {
        pad.SetActive(true);
        leftHorizontal.SetActive(true);
    }

    public void ShowLeftVertical() {
        pad.SetActive(true);
        leftVertical.SetActive(true);
    }

    public void ShowSin() {
        pad.SetActive(true);
        sin.SetActive(true);
    }

    public void ShowSquare() {
        pad.SetActive(true);
        square.SetActive(true);
    }

    public void ShowSawtooth() {
        pad.SetActive(true);
        sawtooth.SetActive(true);
    }

    public void ShowTriangle() {
        pad.SetActive(true);
        triangle.SetActive(true);
    }

    public void Hide() {
        foreach (Transform child in transform) { child.gameObject.SetActive(false); }
        pad.SetActive(false);
    }
}
