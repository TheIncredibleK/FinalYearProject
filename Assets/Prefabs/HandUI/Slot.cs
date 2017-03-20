using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {
	public int amount = 0;
	public int cost = 1;
	public int costIncrease = 2;
	int currentAmount;



	bool CheckIfCanPurchase() {
		if (amount >= cost) {
			return true;
		}
		return false;
	}
	void UpdateText() {
		this.transform.Find("Amount").GetComponent<Text>().text = amount.ToString();
	}

	public void Increase(int increaseAmount) {
		amount += increaseAmount;
		Debug.Log (amount + " : " + increaseAmount);
		UpdateText ();

	}

	public void Decrease(int decreaseAmount) {
		amount -= decreaseAmount;
		UpdateText ();
	}

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
