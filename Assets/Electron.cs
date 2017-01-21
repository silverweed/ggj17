using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Electron : MonoBehaviour {

	public int life = 1;

	void OnTriggerEnter2D(Collider2D coll) {

		if(coll.GetComponent<Wall>()!=null){
			if (--life == 0) {
				StartCoroutine(Die());
			}
			return;
		}
		if(coll.GetComponent<PowerUp>()!=null){
			//Todo do something
			coll.GetComponent<PowerUp>().Pickup();	
		}
	}

	IEnumerator Die() {
		GetComponent<SpriteRenderer>().enabled = false;
		var ps = GetComponentInChildren<ParticleSystem>();
		var vel = ps.velocityOverLifetime;
		var rate = new ParticleSystem.MinMaxCurve();
		rate.constantMax  = GameObject.FindObjectOfType<WavePls>().speed;
		vel.x = rate;
		ps.Play();
		while (ps.isPlaying) {
			yield return null;
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
