using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		coll.enabled = false;
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut() {
		var image = GameObject.Find("WhiteImage").GetComponent<Image>();
		float a = 0f;
		while (a < 1f) {
			image.color = new Color(1f, 1f, 1f, a);
			a += 0.015f;
			yield return new WaitForSeconds(0.025f);
		}
		var curScene = SceneManager.GetActiveScene();
LevelsScroller.LastCompletedLevel(curScene.name);
		SceneManager.LoadScene("Selection");
	}
}
