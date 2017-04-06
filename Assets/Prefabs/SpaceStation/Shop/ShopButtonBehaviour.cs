using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopButtonBehaviour : MonoBehaviour {


	public int myId;
	public int groupId;
	bool isTouching = false;
	public string traderTag;
	int amountAbleToPurchase = 0;
	public string iAmThis;
	public float rarityOfBuy;
	public float rarityOfMe;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		UpdateText ();
		
	}

	//If you trade, then trade and increase rarirty
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Hand") {
			if (!isTouching) {
				GameObject.FindGameObjectWithTag (traderTag).GetComponent<TraderGroup> ().Trade (myId, groupId, amountAbleToPurchase);
				UpdateText ();
				isTouching = true;
				rarityOfMe += rarityOfMe * .15f;
				StartCoroutine (WaitToBuyAgain ());
			}


		}
	}

	void UpdateText() {
		this.transform.parent.Find ("Text").GetComponent<Text>().text = CreateButtonString();
	}

	int ReCalculatePrice() {
		//This calculation takes the 'rarity' of the two objects in terms of how much iron, where iron = 1
		//It then calculates how much of the object you want to buy you can afford with the object you are selling
		float amount = ((GetAmountInInventory() * rarityOfBuy)/rarityOfMe);
		if (amount < 0) {
			amount = 0;
		}
		amountAbleToPurchase = (int)amount;
		return amountAbleToPurchase;
	}

	//Creates string used for button
	string CreateButtonString() {
	
		return "Buy " + ReCalculatePrice () + " " + iAmThis;
	}

	//Gets the amount of a particular item id that exists within inventory
	int GetAmountInInventory() {
		return GameObject.FindGameObjectWithTag (traderTag).GetComponent<TraderGroup> ().GetInventory (groupId);
	}

	//Must wait before re trading
	IEnumerator WaitToBuyAgain() {
		yield return new WaitForSeconds (1.5f);
		isTouching = false;
	}
}
