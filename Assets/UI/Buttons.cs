using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
	
	public GameObject credits;

	public void Play() {
		SceneManager.LoadSceneAsync("Main");
	}

	public void Credits() {
		credits.SetActive(true);
	}

	public void CloseCredits() {
		credits.SetActive(false);
	}

	public void Quit() {
		Application.Quit();
	}
}
