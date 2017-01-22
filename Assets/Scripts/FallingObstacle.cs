using UnityEngine;
using System;

[RequireComponent (typeof(Renderer))]
public class FallingObstacle : MonoBehaviour{

	public Vector2 velocity;
	Collider2D myRend;
	void Start(){
		this.tag = "Wall";
		myRend = GetComponent<Collider2D>();
	}

	void Update(){
		transform.position =(Vector2)((Vector2)transform.position + velocity*Time.deltaTime);
		ClampPositionWithCamera();
	}

	void ClampPositionWithCamera(){
		if(transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - myRend.bounds.extents.y/2f ){
		transform.position = new Vector3(transform.position.x,
				    transform.position.y + (Camera.main.orthographicSize *2f - myRend.bounds.extents.y/2f),
					transform.position.z
					);
		}
		//if(transform.position.y > Camera.main.transform.position.y + Camera.main.orthographicSize + myRend.bounds.extents.y/2f){
			//transform.position = new Vector3(transform.position.x,
					//transform.position.y - (Camera.main.orthographicSize *2f + myRend.bounds.extents.y/2f),
					//transform.position.z
					//);
		//}
	}
}
