﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Electron : MonoBehaviour {

	public int life = 1;
	public AudioClip hitSound;

	CheckpointSystem checkpoint;
	public bool currentlyDestroyed = false;

	void Awake() {
		checkpoint = GameObject.FindObjectOfType<CheckpointSystem>();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Wall") {
			if (!currentlyDestroyed) {
				--life;
				StartCoroutine(Die());
			}
		}
		if (coll.GetComponent<PowerUp>() != null) {
			//Todo do something
			coll.GetComponent<PowerUp>().Pickup();
		}
	}

	IEnumerator Die() {
		currentlyDestroyed = true;
		GetComponent<SpriteRenderer>().enabled = false;
		var ps = GetComponentInChildren<ParticleSystem>();
		var vel = ps.velocityOverLifetime;
		var rate = new ParticleSystem.MinMaxCurve();
		rate.constantMax  = GameObject.FindObjectOfType<Wave>().speed;
		vel.x = rate;
		ps.Play();
		AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
		while (ps.isPlaying) {
			yield return null;
		}
		if (life <= 0) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		} else {
			GetComponent<SpriteRenderer>().enabled = true;
			currentlyDestroyed = false;
			checkpoint.MoveToLastCheckpoint();
		}
	}
}
