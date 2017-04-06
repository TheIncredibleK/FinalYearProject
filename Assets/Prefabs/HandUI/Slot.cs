using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {
	//Amount contained in slot and the basic costs
	public int amount = 0;
	public int cost = 1;
	public int costIncrease = 2;
	int currentAmount;


	//Ensure that the user can actually afford the object
	bool CheckIfCanPurchase() {
		if (amount >= cost) {
			return true;
		}
		return false;
	}

	//Update amount counter
	void UpdateText() {
		this.transform.Find("Amount").GetComponent<Text>().text = amount.ToString();
	}
	//Increase amount
	public void Increase(int increaseAmount) {
		amount += increaseAmount;
		UpdateText ();

	}
	//Decrease amount
	public void Decrease(int decreaseAmount) {
		amount -= decreaseAmount;
		UpdateText ();
	}

	//Function to actually buy upgrades
	public bool Purchase() {
		if (CheckIfCanPurchase ()) {
			Decrease (cost);
			cost *= costIncrease;
			UpdateText ();
			return true;
		}
		return false;
	}

	public void Exchange() {

	}
}
