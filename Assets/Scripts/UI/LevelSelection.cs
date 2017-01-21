using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {

	List<GameObject> levels;
	GameObject curLevel;
	//int 
	JoystickListener joyLst;

	void Start() {
		foreach (var img in GetComponentsInChildren<Image>())
			levels.Add(img.gameObject);
		curLevel = levels[0];
		joyLst = gameObject.AddComponent<JoystickListener>();
		joyLst.OnJoystickLeft += Scroll(true);
		joyLst.OnJoystickRight += Scroll(false);
	}
	
	void Update() {
		
	}

	System.Action Scroll(bool left) {
		if (left) {
			return () => {
				var finalPos = curLevel.transform.position;
				//StartCoroutine(ScrollFromTo(level
			};
		}
		return () => {};
	}
}
