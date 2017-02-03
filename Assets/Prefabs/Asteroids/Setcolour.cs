using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setcolour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Renderer[] children = this.GetComponentsInChildren<Renderer> ();
		Debug.Log (this.transform.childCount);
		Color c =  new Color (Random.value, Random.value, Random.value, 1.0f);
		for (int i = 0; i <= this.transform.childCount + 1; i++) {
			children [i].material.color = c;
		}
	}

}
