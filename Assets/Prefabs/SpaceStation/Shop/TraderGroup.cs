using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderGroup : MonoBehaviour {

	//Overall controller for increasing and decreasing
	//Inventory amounts based on trades
	public GameObject[] InventorySlots;
	// Use this for initialization
	void Start () {
		
	}

	public void Trade(int buyId, int sellId, int increaseAmt) {
		// To buy, I increase one slot by an amount
		InventorySlots[buyId].GetComponent<Slot>().Increase(increaseAmt);
		//Then decrease other to 0.
		InventorySlots[sellId].GetComponent<Slot>().Decrease(InventorySlots[sellId].GetComponent<Slot>().amount);
	}

	public int GetInventory(int getId) {
		return InventorySlots [getId].GetComponent<Slot> ().amount;
	}
}
