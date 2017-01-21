using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour{
	
	public void Pickup(){
		GetComponent<AudioSource>().Play();
		StartCoroutine(animateAndDestroy());
	}

	IEnumerator animateAndDestroy(){
		yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
		Destroy(gameObject);
	}
}
