using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
	//actual manager that controls if shop is active or not

	GameObject player;
	public GameObject shop;
	bool shipOn = false;

	//Public
	public float threshold;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		StartCoroutine (CheckIfShopActive ());
	}
	
	// Update is called once per frame
	void Update () {
		if (shipOn) {
			ActivateShop ();
		} else {
			DeactivateShop ();
		}
		
	}

	void ActivateShop() {
		shop.GetComponent<Shop> ().Activate ();
	}

	void DeactivateShop() {
		shop.GetComponent<Shop> ().Deactivate ();
	}

	IEnumerator CheckIfShopActive() {
		while (true) {
			if (Vector3.Distance (this.transform.position, player.transform.position) < threshold) {
				shipOn = true;
			} else {
				shipOn = false;
			}

			yield return new WaitForSeconds (2.0f);
		}

	}
}
