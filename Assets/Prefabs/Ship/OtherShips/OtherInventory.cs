using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherInventory : MonoBehaviour {

	//Player
	GameObject player;

	//Tracked Slots
	public float health;
	public float armour;
	public float power;
	public float[] Items;
	public float stddmg;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void DamageHealth() {
		health -= stddmg;
	}
}
