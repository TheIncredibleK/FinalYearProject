using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	//Private
	public GameObject myslot;
	public string myName;
	GameObject player;
	float collect_dist = 2.0f;
	float speed = 10.0f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		myslot = GameObject.FindGameObjectWithTag (myName);
	}

	void Update() {
		CheckIfBeingCollected ();
	}
	
	// Update is called once per frame
	void UpdateAmount() {
		Debug.Log ("About to try");
		myslot.GetComponent<Slot> ().Increase (1);
		Debug.Log ("Amount Updated");
	}


	void BeCollected() {
		Debug.Log ("Update");
		UpdateAmount ();
		Debug.Log ("Got out");
		DestroyImmediate (this.gameObject);
		Debug.Log ("Destroyed");

	}

	void CheckIfBeingCollected() {
		float myDist = Vector3.Distance (this.transform.position, player.transform.position);
		//Debug.Log("My Dist : " + myDist);
		if(myDist < player.GetComponent<ShipStatus>().magnet) {
			if (myDist < collect_dist) {
				BeCollected ();
			} else {
				this.transform.position += (player.transform.position - this.transform.position).normalized * speed * Time.deltaTime;
			}
		}
	
	}
}
