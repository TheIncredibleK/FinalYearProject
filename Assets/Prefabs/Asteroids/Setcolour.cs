using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setcolour : MonoBehaviour {
	public Color colour;
	public bool randomiseColor;
	public bool randomiseSections;
	// Use this for initialization
	void Start () {
		Renderer[] children = this.GetComponentsInChildren<Renderer> ();
		if (randomiseColor) {
			
			colour = new Color (Random.value, Random.value, Random.value, 1.0f);
		}
		for (int i = 0; i <= this.transform.childCount + 1; i++) {
			if (randomiseSections) {
				colour = new Color (Random.value, Random.value, Random.value, 1.0f);
			}
			children [i].material.color = colour;
		}
	}

}
