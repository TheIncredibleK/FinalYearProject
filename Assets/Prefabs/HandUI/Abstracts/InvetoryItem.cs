using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InvetoryItem : MonoBehaviour {
	public int amount;
	public float value;
	public string desc;
	Texture2D icon;



	void Remove(int toRemove) {
		this.amount -= toRemove;
	}

	void Add(int toRemove) {
		this.amount += toRemove;
	}
}
