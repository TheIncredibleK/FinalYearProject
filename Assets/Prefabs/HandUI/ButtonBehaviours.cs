using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviours : MonoBehaviour {

	public GameObject image;
	public GameObject mySlot;
	bool isTouching = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Hand") {
			Debug.Log ("Hand");
			if (!isTouching) {
				if (mySlot.GetComponent<Slot> ().Purchase ()) {
					image.GetComponent<UIBarManager> ().IncreaseSize ();
					isTouching = true;
					StartCoroutine (WaitToBuyAgain ());
				}
			}

		}
	}

	IEnumerator WaitToBuyAgain() {
		yield return new WaitForSeconds (1.5f);
		isTouching = false;
	}
}
