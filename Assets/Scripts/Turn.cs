using UnityEngine;
using System;

public class Turn : MonoBehaviour{

	private Vector3 prevPosition;

	void Start(){
		prevPosition = transform.position;
	}
	void Update(){
		Vector2 dir = (Vector2)(transform.position - prevPosition);
		transform.right = dir.normalized;
		prevPosition = transform.position;
	}

}
