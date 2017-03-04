using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	//Public
	public GameObject inventory_slot;

	//Private
	GameObject slot_panel;
	int slot_amount;
	List<GameObject> slots = new List<GameObject>();


	// Use this for initialization
	void Start () {
		slot_amount = 6;
		slot_panel = this.transform.Find("Slot Panel").gameObject;

		for (int i = 0; i < slot_amount; i++) {
			slots.Add (Instantiate(inventory_slot));
			slots [i].transform.SetParent(slot_panel.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
