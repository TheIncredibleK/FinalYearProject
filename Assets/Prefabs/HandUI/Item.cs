using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	//Private
	//The slot in which this item is held
	public GameObject myslot;
	//Name of the slot
	public string myName;
	GameObject player;
	//Distance from which it is collected
	float collect_dist = 2.0f;
	//Speed it moves towards player
	float speed = 10.0f;
	// Use this for initialization
	void Start () {
		Setup ();		
	}

	public void Setup() {
		player = GameObject.FindGameObjectWithTag ("Player");
		myslot = GameObject.FindGameObjectWithTag (myName);

	}

	void Update() {
		CheckIfBeingCollected ();
	}
	
	//Update amount of collected items slot
	void UpdateAmount() {
		myslot.GetComponent<Slot> ().Increase (1);
	}

	//Function called when the item is collected
	void BeCollected() {
		UpdateAmount ();
		DestroyImmediate (this.gameObject);

	}

	//If the item is within the players magentic reach
	//Fly towards player
	//When it is within a collection distance
	//Update amounts and destroy self
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
