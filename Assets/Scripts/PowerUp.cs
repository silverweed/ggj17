using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	
	public void Pickup(){
		GetComponent<AudioSource>().Play();
		GameObject.FindObjectOfType<RollOnPickUp>().PickedUp();
		StartCoroutine(animateAndDestroy());
	}

	IEnumerator animateAndDestroy() {
		yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
		Destroy(gameObject);
	}
}
