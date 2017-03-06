using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	//Private
	int myAmount = 0;
	public GameObject myslot;
	public string myName;
	GameObject player;
	float pull_dist = 5.0f;
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
	void UpdateAmount(int amountChange) {
		myAmount += amountChange;
		myslot.transform.Find ("Amount").GetComponent<Text>().text = myAmount.ToString();
	}


	void BeCollected() {
		Debug.Log ("In to beCollected()");
		UpdateAmount (1);

		Destroy (this.gameObject);

	}

	void CheckIfBeingCollected() {
		float myDist = Vector3.Distance (this.transform.position, player.transform.position);
		//Debug.Log("My Dist : " + myDist);
		if(myDist < pull_dist) {
			if (myDist < collect_dist) {
				Debug.Log ("Making it into if check");
				BeCollected ();
			} else {
				this.transform.position += (player.transform.position - this.transform.position).normalized * speed * Time.deltaTime;
			}
		}
	
	}
}
