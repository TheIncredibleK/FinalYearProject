using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviours : MonoBehaviour {

	//Image for bar
	public GameObject image;
	//Item slot
	public GameObject mySlot;
	public GameObject player;
	//Value to be passed into the ship status to control what overall value gets increased
	public int myFuncKeyValue;
	bool isTouching = false;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	//If the button is presed
	//Try purchase, and if purchase is successful, increase values and then
	//Begin co routine to ensure it doesn't mass buy
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Hand") {
			Debug.Log ("Getting hit : " + this.name);
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
