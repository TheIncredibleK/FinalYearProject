using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviours : MonoBehaviour {

	public GameObject image;
	public GameObject mySlot;
	bool isTouching = false;


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Hand") {
			if (!isTouching) {
				image.GetComponent<UIBarManager> ().IncreaseSize ();
				isTouching = true;
			}

		}
	}

	void OnTriggerExit() {
		Debug.Log ("Untouching");
		isTouching = false;
	}
}
