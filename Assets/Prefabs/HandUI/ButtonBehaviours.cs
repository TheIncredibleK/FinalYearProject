using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviours : MonoBehaviour {

	public GameObject image;
	public GameObject mySlot;
	public GameObject player;
	public int myFuncKeyValue;
	bool isTouching = false;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Hand") {
			if (!isTouching) {
				if (mySlot.GetComponent<Slot> ().Purchase ()) {
					Debug.Log ("Hand");
					player.GetComponent<ShipStatus> ().Increase (image, myFuncKeyValue);
					Debug.Log ("Left ship status");
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
