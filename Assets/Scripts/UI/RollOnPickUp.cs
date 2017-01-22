using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollOnPickUp : MonoBehaviour {

	public Text text;
	int currentPicks =0;

	// Use this for initialization
	void Start () {	
		text.text = ""+currentPicks;
	}
	
	
	// Update is called once per frame
	void Update () {
	}

	public void PickedUp(){
		currentPicks++;
		text.text = ""+currentPicks;
		GetComponent<Animation>().Play("RollOnPick");
	}
}
