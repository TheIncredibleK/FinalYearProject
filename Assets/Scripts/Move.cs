using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] items = GameObject.FindGameObjectsWithTag ("Move");

		for (int i = 0; i < items.Length; i++) {
			items [i].transform.position += items [i].transform.forward * 10.0f * Time.deltaTime;
		}
		
	}
}
