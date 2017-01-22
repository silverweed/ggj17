using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	
	public void Pickup(){
		GetComponent<AudioSource>().Play();
		GameObject.FindObjectOfType<RollOnPickUp>().PickedUp();
		StartCoroutine(animateAndDestroy());
	}

	IEnumerator animateAndDestroy() {
		var ps = GetComponentInChildren<ParticleSystem>();
		var vel = ps.velocityOverLifetime;
		var rate = new ParticleSystem.MinMaxCurve();
		rate.constantMax  = GameObject.FindObjectOfType<Wave>().speed;
		vel.x = rate;
		ps.Play();
		yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
		Destroy(gameObject);
	}
}
