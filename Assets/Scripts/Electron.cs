using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Electron : MonoBehaviour {

	public event System.Action OnDeath, OnRespawn;

	public int life = 1;
	public AudioClip hitSound;

	CheckpointSystem checkpoint;
	public bool currentlyDestroyed = false;
	Animator anim;

	void Awake() {
		checkpoint = GameObject.FindObjectOfType<CheckpointSystem>();
	}

	void Start(){
		anim = GetComponent<Animator>();
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Wall") {
			if (!currentlyDestroyed) {
				--life;
				if (OnDeath != null)
					OnDeath();
				StartCoroutine(Die());
			}
		}
		if (coll.GetComponent<PowerUp>() != null && !currentlyDestroyed) {
			//Todo do something
			coll.GetComponent<PowerUp>().Pickup();
		}
	}

	public void ChangedWaveShape(Wave.Shape newshape){
        // if newshape != current then do something
		anim.SetBool("triangle", newshape == Wave.Shape.SAW || newshape == Wave.Shape.TRIANGLE);
		anim.SetBool("square" , newshape == Wave.Shape.SQUARE);
		anim.SetBool("circle", newshape == Wave.Shape.SINE);
		Debug.Log(newshape.ToString());
	}

	IEnumerator Die() {
		currentlyDestroyed = true;
		foreach (var child in GetComponentsInChildren<SpriteRenderer>()){
			child.enabled = false;
		}
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
			foreach (var child in GetComponentsInChildren<SpriteRenderer>()) {
				child.enabled = true;
			}
			//GetComponent<SpriteRenderer>().enabled = true;
			currentlyDestroyed = false;
			checkpoint.MoveToLastCheckpoint();
			if (OnRespawn != null)
				OnRespawn();
		}
	}
}
