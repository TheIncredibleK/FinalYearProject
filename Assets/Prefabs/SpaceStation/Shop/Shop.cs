using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
	//spawns shop based on distance from sphere in space station

	bool active = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeState(float edit, bool state) {
		this.transform.GetChild (0).gameObject.SetActive (state);
		float threshold = 1.0f * Mathf.Sign (edit);
		float a = this.GetComponent<Renderer> ().material.color.a;
		Color color = this.GetComponent<Renderer> ().material.color;
		Debug.Log ("a : " + a);

		while (ContinueWhile(edit, a)) {
				a += edit;
				color.a = a;
				this.GetComponent<Renderer> ().material.color = color;
			}

	}

	bool ContinueWhile(float edit, float a) {
		if (Mathf.Sign (edit) > 0) {
			if(a < 1.0f) {
				return true;
			}
		} else {
			if(a > 0.0f) {
				return true;
			}
		}

		return false;
	}

	public void Deactivate() {
		this.gameObject.SetActive (false);

	}

	public void Activate() {
		this.gameObject.SetActive (true);
	}
}
