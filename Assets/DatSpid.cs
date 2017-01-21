using UnityEngine;

/// <summary>
/// How to speed.
/// </summary>
public class DatSpid : MonoBehaviour {

    Wave wave;

    void Start() {
        wave = GameObject.FindObjectOfType<Wave>();
    }

	void Update () {
        transform.Translate(-wave.speed * Proportion() * Time.deltaTime, 0f, 0f);
	}

    float Proportion() {
        var left = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0));
        return (right.x - left.x) / Screen.width;
    }
}
