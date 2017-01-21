using UnityEngine;

public class TimeDistortionCuzWhyNot : MonoBehaviour{

	void Start(){
		Destroy(this);
	}
	void Update(){
		print(Input.GetAxis("Fire1"));
		if(Input.GetAxis("Fire1")>0){
				print("Fire1");
				Time.timeScale = Mathf.Clamp( 0f, 2f, Time.timeScale - Time.deltaTime * Input.GetAxis("Fire1"));
			}
		if(Input.GetAxis("Fire2")>0){
				print("Fire2");
				Time.timeScale = Mathf.Clamp( 0f, 2f, Time.timeScale + Time.deltaTime*Input.GetAxis("Fire2"));
		}
		print("time scale is "+ Time.timeScale);
	}

}
